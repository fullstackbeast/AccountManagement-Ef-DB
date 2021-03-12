using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public abstract class Details : BaseEntity
    {
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

       


    }
}
