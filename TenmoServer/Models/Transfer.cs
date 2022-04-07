﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }

        public int TransferType { get; set; }

        public int TransferStatusId { get; set; }

        public int AccountFrom { get; set; }

        public int AccountTo { get; set; }

        public decimal Amount { get; set; }

    }
}
