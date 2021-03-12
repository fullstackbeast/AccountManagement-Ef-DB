using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSEFDB.Interface.Service
{
    interface IAccountHolderService
    {

        public bool CreateAccountHolder(string firstName, string lastName, string middleName, DateTime dateOfBirth, string email, string phoneNumber, string address, string password, string checkPassword);

        //public bool UpdateAccountHolder(int id, string firstName, string middleName, string lastName, string email, string address, string phoneNumber, int pin);

        //public bool RemoveAccountHolder(int id);

        //public List<AccountHolder> ListAccountHolders();

        //public void DisplayAll();

        ////public AccountHolder FindByIdOrEmail(int id, string email);

        //public AccountHolder FindById(int id);

        public int LogInAccountholder(string email, string password);

       

        //public void LogoutAccountholder();
        //public void GetAccountDetails(string email);

    }
}
