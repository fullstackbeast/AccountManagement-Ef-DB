using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public abstract class LoanOverdraftDetails: BaseEntity
    {

        public AccountHolder AccountHolder { get; set; }

        public int AccountHolderId { get; set; }

        public double Amount { get; set; }

        public int Status { get; set; }


       
    }
}
