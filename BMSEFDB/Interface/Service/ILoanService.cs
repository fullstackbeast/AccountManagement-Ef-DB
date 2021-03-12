using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB.Interface.Service
{
    public interface ILoanService
    {
        public void AddLoan(int accountHolderId, double amount, string type);

        public Loan UpdateLoan(Loan loan);
        
    }
}
