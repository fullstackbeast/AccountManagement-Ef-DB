using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSEFDB.Interface.Repository
{
    public interface IAccountRepository
    {
        //public List<Account> ListAccounts();
        public bool Create(Account account);
        public Account FindById(int id);
        public Account Update(Account account);

        public bool UpdateMultiple(List<Account> accounts);
        public Account FindByAccountNumber(string accountNumber);
        //public bool Update(Account account, int accountHolderId);
        //public Account FindByAccountNumber(string accountNumber);

    }
}
