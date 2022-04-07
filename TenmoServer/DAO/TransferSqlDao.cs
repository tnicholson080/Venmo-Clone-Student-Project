using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDAO
    {
        private readonly string connectionString;
        public TransferSqlDao(string connString)
        {
            connectionString = connString;
        }

        public List<Transfer> GetListOfTransfers(int account)
        {
           
                Transfer transfer = null;
                List<Transfer> transfers = new List<Transfer>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfer t JOIN account a " +
                        "ON t.account_from = a.account_id OR t.account_to = a.account_id " +
                        "WHERE a.user_id = @account_to OR a.user_id = @account_from", conn);
                    cmd.Parameters.AddWithValue("@account_to", account);
                    cmd.Parameters.AddWithValue("@account_from", account);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        transfer = CreateTransferFromReader(reader);
                    transfers.Add(transfer);
                    }
                }
                return transfers;
            }

        public void CreateTransfer(int accountTo, int accountFrom, decimal amountToTransfer)
        {
            Transfer transfer = new Transfer();
            int userId = 1;
            /*//we will update both balances here
            //check balance so we can't overdraw
            //need 2 select statements, one to get get to account and one to get from account, then plug values into variables and add them into 3rd sql statement for insert
          
               

            }*/
          
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO transfer (account_to, account_from, amount, transfer_type_id, transfer_status_id ) OUTPUT INSERTED.transfer_id " +
                    "VALUES ((SELECT account_id FROM account WHERE account.user_id = @account_to ), " +
                    "(SELECT account_id FROM account WHERE account.user_id = @account_from), @amount, @transfer_type_id, @transfer_status_id); " +
                    "UPDATE account SET balance = balance - @amount WHERE account_id = (SELECT account_id FROM account WHERE user_id = @account_from);" +
                    "UPDATE account SET balance = balance + @amount WHERE account_id = (SELECT account_id FROM account WHERE user_id = @account_to);", conn);
          
                cmd.Parameters.AddWithValue("@account_to", accountTo);
                cmd.Parameters.AddWithValue("@account_from", accountFrom);
                cmd.Parameters.AddWithValue("@amount", amountToTransfer);
                cmd.Parameters.AddWithValue("@transfer_type_id", 1);
                cmd.Parameters.AddWithValue("@transfer_status_id", 1);

                cmd.ExecuteNonQuery();

            }
           /* using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE account SET balance = ")
            }*/
        }

            private Transfer CreateTransferFromReader(SqlDataReader reader)
                {
                    Transfer transfer = new Transfer();
                    transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
                    transfer.TransferType = 1;
                    transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
                    transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
                    transfer.Amount = Convert.ToDecimal(reader["amount"]);
                    transfer.TransferStatusId = 1;
                    return transfer;
                }

        public Transfer GetTransfer(int transferId)
        {
            Transfer transfer = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @transfer_id;", conn);
                cmd.Parameters.AddWithValue("@transfer_id", transferId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    transfer = CreateTransferFromReader(reader);
                   
                }
            }
            return transfer;

        }
    }
}

