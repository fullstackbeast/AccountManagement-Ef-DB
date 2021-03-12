using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMSEFDB.Interface.Repository;

namespace BMSEFDB.Domain.Repository
{
    public class OverdraftRepository : IOverdraftRepository
    {
        BankDBContext context = BankDBContext.Connect(Constants.ConnectionString);

        public Overdraft FindActiveOverdraftById(int id)
        {
            return context.Overdrafts.Where(ov => ov.AccountHolderId == id && ov.Status == 1).FirstOrDefault();
        }

        public List<Overdraft> FindAllOverdraftById(int id)
        {
            return context.Overdrafts.Where(ov => ov.AccountHolderId == id).ToList();
        }

        public Overdraft GetOverdraft(Overdraft overdraft)
        {
            context.Overdrafts.Add(overdraft);
            context.SaveChanges();
            return overdraft;
        }

        public Overdraft UpdateOverdraft(Overdraft overdraft)
        {
            context.Overdrafts.Update(overdraft);
            context.SaveChanges();
            return overdraft;
        }
    }
}
