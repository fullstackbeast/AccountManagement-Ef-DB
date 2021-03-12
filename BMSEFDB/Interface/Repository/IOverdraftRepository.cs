using System;
using System.Collections.Generic;
using System.Text;

namespace BMSEFDB.Interface.Repository
{
    public interface IOverdraftRepository
    {
        public Overdraft GetOverdraft(Overdraft overdraft);
        public Overdraft UpdateOverdraft(Overdraft overdraft);
        public List<Overdraft> FindAllOverdraftById(int id);
        public Overdraft FindActiveOverdraftById(int id);
    }
}
