using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MySql.Data.MySqlClient;
using BMSEFDB.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace BMSEFDB
{
    public class AccountRepository : IAccountRepository
    {


        BankDBContext context = BankDBContext.Connect(Constants.ConnectionString);




        public static Account LoggedInAccount;
        

        public bool Create(Account account)
        {

           
                context.Accounts.Add(account);
                context.SaveChanges();
                return true;
        }

        public Account FindById(int id)
        {
            return context.Accounts.FirstOrDefault(a => a.AccountHolderId == id);
        }

        public Account Update(Account account)
        {
            context.Accounts.Update(account);
            context.SaveChanges();
            return account;
        }

        public bool UpdateMultiple(List<Account> accounts)
        {
            context.Accounts.UpdateRange(accounts);
            context.SaveChanges();
            return true;
        }

        public Account FindByAccountNumber(string accountNumber)
        {
            return context.Accounts.Where(an => an.AccountNumber == accountNumber).Include(ac => ac.AccountHolder).FirstOrDefault();
        }

     

 

    

    }



}
