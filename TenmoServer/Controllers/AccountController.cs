/*using Microsoft.AspNetCore.Components;*/
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("account")] /* ********************************* */
    [ApiController]
   
    
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO accountDAO; 

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Account> GetAccountById(int id)
        {
            Account account = accountDAO.GetAccount(id);
            if (account != null)
            {
                return account;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("balance")]
        public decimal GetBalance()
        {
            string userId = User.FindFirst("sub")?.Value;
            int id = Convert.ToInt32(userId);
            return accountDAO.GetAccountBalance(id);
        }

      
    }
}
