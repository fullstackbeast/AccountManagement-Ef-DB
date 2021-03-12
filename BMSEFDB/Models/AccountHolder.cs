using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public class AccountHolder : Details
    {
        public AccountHolder()
        {
            Loans = new List<Loan>();
            OverDrafts = new List<Overdraft>();
        }
        
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public IList<Loan> Loans { get; set; } 

        public IList<Overdraft> OverDrafts { get; set; } 
        public Account Account { get; set; }
    }
}
