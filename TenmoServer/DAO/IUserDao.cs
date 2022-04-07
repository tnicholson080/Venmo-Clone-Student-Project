using System.Collections.Generic;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password);
        List<User> GetUsers(int userId);
        public string GetUsernameByAccountId(int accountId);

        /*      int GetMyID(int userId);*/
    }
}
