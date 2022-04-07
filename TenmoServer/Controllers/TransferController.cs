using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
  /* *********************************** */  [Route("transfer")] /* ***************************************** */

  [ApiController]

    public class TransferController : ControllerBase
    {
        private readonly ITransferDAO transferDAO;

        public TransferController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }

        [HttpGet()]
        public  ActionResult<List<Transfer>> ListTransfers()
        {
          
            return transferDAO.GetListOfTransfers(GetUserIdFromToken());
        }
/*
        [HttpGet("{id}")]
        public Transfer GetTransfer(int id)
        {

        }
 */

        [HttpPost()]
        public void CreateTransfer(Transfer transfer)
        {
            transferDAO.CreateTransfer(transfer.AccountTo, GetUserIdFromToken(), transfer.Amount);
        }


        public int GetUserIdFromToken()
        {
            int userId = -1;
            try
            {
                userId = int.Parse(User.FindFirst("sub")?.Value);
            }
            catch
            {
            }
            return userId;
        }

    }
}
