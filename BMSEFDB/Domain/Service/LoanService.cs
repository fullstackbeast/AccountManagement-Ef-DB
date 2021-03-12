using System;
using System.Collections.Generic;
using System.Text;
using BMSEFDB.Interface.Repository;
using BMSEFDB.Interface.Service;

namespace BMSEFDB.Domain.Service
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

      
        public void AddLoan(int accountHolderId, double amount, string type)
        {

            Loan newLoan = new Loan
            {
                AccountHolderId = accountHolderId,
                Amount = amount,
                Status = 1,
                TypeOfLoan = type,
                AmountLeft = amount
              
            };

            try
            {

                List<Loan> allLoan = _loanRepository.FindAllLoansById(accountHolderId);

                if (allLoan.Count < 1)
                {

                    _loanRepository.CreateLoan(newLoan);
                }
                else
                {
                    Loan activeLoan = FindActiveLoan(allLoan);
                    if (activeLoan == null){
                        _loanRepository.CreateLoan(newLoan);
                         Console.WriteLine($"You Have Succesfully Being Grant A {type} Loan of {amount}");
                    }

                    else
                    {
                        throw new Exception($"You currently have an unpaid loan of {activeLoan.AmountLeft}. Please pay up to qualify for another");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void PayLoan(int accountHolderId, double amount)
        {

            Console.WriteLine(accountHolderId);
            Console.WriteLine(amount);

            List<Loan> allLoan = _loanRepository.FindAllLoansById(accountHolderId);

            if (allLoan.Count < 1 || (FindActiveLoan(allLoan) == null))
            {
                Console.WriteLine("You do not have any active loan ):");
            }
            else
            {

                Loan activeLoan = FindActiveLoan(allLoan);


                if (activeLoan.AmountLeft < amount)
                {
                    Console.WriteLine("Amount To Pay Is Greater Than Your Loan Balance");
                }
                else
                {
                    activeLoan.AmountLeft -= amount;
                    if (activeLoan.AmountLeft <= 0) activeLoan.AmountLeft = 0;

                    if (activeLoan.AmountLeft == 0) activeLoan.Status = 0;

                    if (_loanRepository.UpdateLoan(activeLoan) != null)
                    {
                        string message = (activeLoan.Status == 0) ? $"Congratulations: Your Loan has been completely paid." : $"You have successfully paid {amount}. You currently have {activeLoan.AmountLeft} left to pay";

                        Console.WriteLine(message);
                    }
                    else
                    {
                        Console.WriteLine("An eeror occoured");
                    }

                }

            }

        }
        public double checkLoanBalance(int accountHolderId)
        {
            double balance = 0;

            Loan activeLoan = _loanRepository.FindActiveLoanById(accountHolderId);

            if (activeLoan != null)
            {
            
                    balance = activeLoan.AmountLeft;
                
            }

            return balance;
        }
        public Loan FindActiveLoan(List<Loan> allLoan)
        {
            return allLoan.Find(x => x.Status == 1);
        }

        public Loan UpdateLoan(Loan loan)
        {
            return _loanRepository.UpdateLoan(loan);
        }
    }
}
