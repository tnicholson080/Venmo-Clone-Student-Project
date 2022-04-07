using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        public Transfer GetTransfer(int transferId);
        public void CreateTransfer(int accountTo, int accountFrom, decimal amountToTransfer);

        public List<Transfer> GetListOfTransfers(int accountTo);

     
    }
}
