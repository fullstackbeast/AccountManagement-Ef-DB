using System;
using System.Collections.Generic;
using System.Text;
using BMSEFDB.Interface.Repository;
using BMSEFDB.Interface.Service;

namespace BMSEFDB.Domain.Service
{
    public class OverdraftService : IOverdraftService
    {
        private readonly IOverdraftRepository _overdraftRepository;

        public OverdraftService(IOverdraftRepository overdraftRepository)
        {
            _overdraftRepository = overdraftRepository;
        }

        public void GetOverdraft(int accountHolderId, double amount, double amountLeft)
        {
            Overdraft newOverdraft = new Overdraft {
                AccountHolderId = accountHolderId,
                Amount = amount,
                Status = 1,
                AmountLeft = amountLeft
            };

            _overdraftRepository.GetOverdraft(newOverdraft);
        }

        public void PayOverdraft(int accountHolderId, double amount)
        {
            Overdraft activeoverdraft = _overdraftRepository.FindActiveOverdraftById(accountHolderId);
            if(activeoverdraft != null)
            {
                activeoverdraft.AmountLeft -= amount;

                if (activeoverdraft.AmountLeft <= 0) activeoverdraft.AmountLeft = 0;
                if (activeoverdraft.AmountLeft == 0) activeoverdraft.Status = 0;

                _overdraftRepository.UpdateOverdraft(activeoverdraft);
            }
       

         
        }

        public bool HasActiveOverdraft(int accountHolderId)
        {
            return _overdraftRepository.FindActiveOverdraftById(accountHolderId) != null;
        }

    }
}
