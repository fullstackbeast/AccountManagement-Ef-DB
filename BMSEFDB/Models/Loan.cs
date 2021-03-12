using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public class Loan : LoanOverdraftDetails
    {

        public string TypeOfLoan { get; set; }

        public double AmountLeft { get; set; }
        public DateTime LoanDate { get; }

        

    }







}

