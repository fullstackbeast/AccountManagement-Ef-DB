 
using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB
{
    public interface IAccountHolderRepository
    {
        public  int CreateAccountHolder(AccountHolder accountHolder);

        public AccountHolder FindByEmail(string email);
        public AccountHolder GetDetails(int id);
        //public AccountHolder GetDetailsByAccountNumber(string accountNumber);
        public AccountHolder FindById(int id);
        public AccountHolder Update(AccountHolder accountHolder);
        public AccountHolder GetDetailsByEmail(string email);

        public AccountHolder GetAccountHolderOverdraftDetails(int accountHolderId);




        //public bool UpdateAccountHolder(AccountHolder accountHolder);

        //public bool RemoveAccountHolder(int id);

        //public List<AccountHolder> ListAccountHolders();

        //public void DisplayAll();

        ////public AccountHolder FindByIdOrEmail(int id, string email);

        //public AccountHolder FindById(int id);

        //public AccountInfo GetAccountInfo(string email);

        //public List<AccountInfo> GetAllAccountHolders();

    }
}
