using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao userDao)
        {
            this.userDao = userDao;
        }
        
        [HttpGet]
        public List<User> GetUsers()
        {
            string userId = User.FindFirst("sub")?.Value;
            int id = Convert.ToInt32(userId);
            
            List<User> users = userDao.GetUsers(id);
            return users;
        }

        [HttpGet("{accountId}")]
        public string GetUsernameByAccountId(int accountId)
        {
            return userDao.GetUsernameByAccountId(accountId);
        }
    }
}
