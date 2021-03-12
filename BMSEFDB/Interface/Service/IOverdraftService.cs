using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB.Interface.Service
{
    public interface IOverdraftService
    {
        public void GetOverdraft(int accountHolderId, double amount, double amountLeft);

        public void PayOverdraft(int accountHolderId, double amount);
    }
}
