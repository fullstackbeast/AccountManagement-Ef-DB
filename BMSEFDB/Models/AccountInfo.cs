using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSEFDB
{
    public class AccountInfo : Details
    {
        //it is a database object
        public DateTime DateOfBirth { get; set; }

        public int IdOfAcc { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string AccountNumber { get; set; }

        public double AccountBalance { get; set; }

        public int AccountHolderId { get; set; }

        public int Pin { get; set; }

        public int AccountStatus { get; set; }

       

        

      
    }
}
