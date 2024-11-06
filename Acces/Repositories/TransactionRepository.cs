using Acces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Acces.Repositories
{
    public class TransactionRepository
    {
        private LeavesDbContext _leavesDbContext;

        public TransactionRepository( LeavesDbContext leavesDbContext )
        {
            _leavesDbContext = leavesDbContext;
        }

        public void CreateTransaction(int accountIdFrom, int accountIdTo, decimal Amount) 
        {
            _leavesDbContext.Tranasactions.Add(new Transaction
            {
                FkAccountidfrom = accountIdFrom,
                FkAccountidto = accountIdTo,
                Amount = Amount
            });
        }
        public IEnumerable<Transaction> GetAccountTransactions(int id)
        {
            return _leavesDbContext.Accounts.Include(x => x.TransactionFkAccountidfromNavigations)
                .SingleOrDefault(x => x.PkAccountid == id).TransactionFkAccountidfromNavigations;
         
        }
        public IEnumerable<Transaction> GetTransactionToAccount(int id)
        {
            return _leavesDbContext.Accounts.Include(x => x.TransactionFkAccountidtoNavigations)
                .SingleOrDefault(x => x.PkAccountid == id).TransactionFkAccountidtoNavigations;
        }
        public IEnumerable<Transaction> GetAccToAccTransactions(int accountIdFrom, int accountIdTo)
        {
            return _leavesDbContext.Accounts.Include(x => x.TransactionFkAccountidfromNavigations)
                .ThenInclude(x => x.FkAccountidto)
                .SingleOrDefault(x => x.PkAccountid == accountIdFrom)
                .TransactionFkAccountidfromNavigations.Where(x => x.FkAccountidto == accountIdTo);
        }
    }
}
