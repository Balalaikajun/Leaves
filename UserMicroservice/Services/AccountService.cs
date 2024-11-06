using Acces.Context;
using Acces.Repositories;
using Data.Models;

namespace UserMicroservice.Services
{
    public class AccountService
    {
        private AccountRepository _accountRepository;
        private LeavesDbContext _dbContext;


        public AccountService(AccountRepository repository, LeavesDbContext dbContext)
        {
            _accountRepository = repository;
            _dbContext = dbContext;

        }

        public void Create(int userId)
        {
            _accountRepository.CreateAccount(userId);
            _dbContext.SaveChanges();
        }

        public Account Get(int accountId)
        {
            return _accountRepository.GetAccountById(accountId);
        }

        public IEnumerable<Account> GetAccounts(int userId)
        {
            return _accountRepository.GetUserAccounts(userId);
        }
    }
}
