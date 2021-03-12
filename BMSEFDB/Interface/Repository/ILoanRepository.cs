using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB.Interface.Repository
{
   public  interface ILoanRepository
    {
        public Loan CreateLoan(Loan loan);
        public Loan UpdateLoan(Loan loan);

        public List<Loan> FindAllLoansById(int id);



        public Loan FindActiveLoanById(int id);
    }
}
