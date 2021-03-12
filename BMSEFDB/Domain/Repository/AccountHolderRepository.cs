
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMSEFDB
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
     
        BankDBContext context = BankDBContext.Connect(Constants.ConnectionString);


        public int CreateAccountHolder(AccountHolder accountHolder)
        {
            try
            {
                context.AccountHolders.Add(accountHolder);
                context.SaveChanges();
                return accountHolder.Id;

          
            }
            catch (Exception ea)
            {
                Console.WriteLine($"err2: {ea.Message}");
            }

            

            return 0;

        }

        public AccountHolder FindByEmail(string email)
        {

            return context.AccountHolders.FirstOrDefault(e => e.Email == email);
        }

        public AccountHolder FindById(int id)
        {
            return context.AccountHolders.FirstOrDefault(i => i.Id == id);
        }

        public AccountHolder Update(AccountHolder accountHolder)
        {
            context.AccountHolders.Update(accountHolder);
            context.SaveChanges();
            return accountHolder;
        }

        public AccountHolder GetDetails(int id)
        {
            var accountHolder = context.AccountHolders.Where(ach => ach.Id == id).Include(ach => ach.Account).FirstOrDefault();
            return accountHolder;
            
        }
        public AccountHolder GetDetailsByEmail(string email)
        {
            var accountHolder = context.AccountHolders.Where(ach => ach.Email == email).Include(ach => ach.Account).FirstOrDefault();
            return accountHolder;

        }

        public AccountHolder GetAccountHolderOverdraftDetails(int accountHolderId)
        {
          return context.AccountHolders.Where(ach => ach.Id == accountHolderId).Include(ach => ach.OverDrafts).FirstOrDefault();
        }



        //public AccountHolder GetDetailsByAccountNumber(string accountNumber)
        //{
        //    AccountInfo accountInfo = null;
        //    var account = context.Accounts.FirstOrDefault(ac => ac.AccountNumber == accountNumber);
        //    if (account == null) return accountInfo;

        //    var accountHolder = context.AccountHolders.Find(account.AccountHolderId);

        //    return accountInfo;
        //}


    }
}