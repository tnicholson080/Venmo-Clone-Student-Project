using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class RequestTransfer
    {
        public int TransferId { get; set; }

        public int TransferType { get; set; }

        public int TransferStatusId { get; set; }

        public int AccountFrom { get; set; }

        public int AccountTo { get; set; }

        public decimal Amount { get; set; }
    }
}
