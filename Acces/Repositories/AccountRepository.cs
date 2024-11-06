using Acces.Context;
using Acces.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acces.Repositories
{
    public class AccountRepository
    {
        private LeavesDbContext _leavesDbContext;


        public AccountRepository(LeavesDbContext leavesDbContext)
        {
            _leavesDbContext = leavesDbContext;
        }

        public void CreateAccount(int userId) 
        {
            _leavesDbContext.Accounts.Add(new Account
            {
                FkUserid = userId,
            });
        }
        public Account GetAccountById(int id)
        {
            return _leavesDbContext.Accounts.SingleOrDefault(x => x.PkAccountid == id);
        }
        public ICollection<Account> GetUserAccounts(int userId)
        {
            return _leavesDbContext.Users.Include(user => user.Accounts).SingleOrDefault(x => x.PkUserid==userId).Accounts;
        }

        public void UpdateAccount(int id, int balance)
        {
            GetAccountById(id).Balance = balance;
        }
        public void DeleteAccount(int id) 
        {
            _leavesDbContext.Accounts.Remove(GetAccountById(id));
        }
    }
}
