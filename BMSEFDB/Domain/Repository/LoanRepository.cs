using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMSEFDB.Interface.Repository;

namespace BMSEFDB.Domain.Repository
{
    public class LoanRepository: ILoanRepository
    {
        BankDBContext context = BankDBContext.Connect(Constants.ConnectionString);

        public Loan CreateLoan(Loan loan)
        {
            context.Loans.Add(loan);
            context.SaveChanges();
            return loan;

        }

        public Loan UpdateLoan(Loan loan)
        {
            context.Loans.Update(loan);
            context.SaveChanges();
            return loan;
        }

        public List<Loan> FindAllLoansById(int id)
        {
            return context.Loans.Where(ln => ln.AccountHolderId == id).ToList();
            
        }

        public Loan FindActiveLoanById(int id)
        {
            return context.Loans.Where(ln => ln.AccountHolderId == id && ln.Status == 1).FirstOrDefault();
        }
    }
}
