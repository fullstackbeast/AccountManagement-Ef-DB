using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public class Overdraft : LoanOverdraftDetails
    {

        public double AmountLeft { get; set; }
        public DateTime OverdraftDate { get; }


       
    }
}
