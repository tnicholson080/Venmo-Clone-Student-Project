using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class DisplayTransfer
    {

        public int TransferId { get; set; }

        public int TransferType { get; set; }

        public int TransferStatusId { get; set; }

        public string SenderUserName { get; set; }

        public string RecieverUserName { get; set; }

        public decimal Amount { get; set; }
    }
}
