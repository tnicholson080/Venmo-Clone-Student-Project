using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDao : IAccountDAO 
    {
        private readonly string connectionString;
        public AccountSqlDao(string connString)
        {
            connectionString = connString;
        }

        public Account GetAccount(int userId)
        {
            {
                Account account = new Account();

          
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT* FROM Account WHERE user_id = @user_id", connection);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        account = CreateAccountFromReader(reader);
                    }

                }
                return account;
            }
        }

        public decimal GetAccountBalance(int userId)
        {
            {
                Account account = new Account();
                
                decimal accountBalance = 0;
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT* FROM Account WHERE user_id = @user_id", connection);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                   
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if(reader.Read())
                    {
                        account = CreateAccountFromReader(reader);
                    }

                    accountBalance = account.Balance;
                
                }
                return accountBalance;
            }
        }

      
        private Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account Account = new Account();
            Account.AccountId = Convert.ToInt32(reader["account_id"]);
            Account.UserId = Convert.ToInt32(reader["user_id"]);
            Account.Balance = Convert.ToDecimal(reader["balance"]);
            return Account;

        }
    }
}
