using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public  class  Account
    {
        public AccountHolder AccountHolder { get; set; }

        public string AccountNumber { get; set; }

        public double AccountBalance { get; set; }

        public int AccountHolderId { get; set; }

        public int Pin { get; set; }

        public int AccountStatus { get; set; }

    }
}
