using Acces.Context;
using Acces.Repositories;
using Data.Models;
using System.Data;

namespace UserMicroservice.Services
{
    public  class UserService
    {
        private UserRepository _userRepository;
        private LeavesDbContext _dbContext;
        private PasswordService _passwordService;

        public UserService( LeavesDbContext dbContext, PasswordService passwordService)
        {
            _userRepository = new UserRepository(dbContext);
            _dbContext = dbContext;
            _passwordService = passwordService;
        }

        public  void CreateUser(string login, string password)
        {
            if(_userRepository.GetUserByLogin(login) != null)
            {
                throw new DuplicateNameException();
            }

            _userRepository.CreateUser(login, _passwordService.Hash(password));

            _dbContext.SaveChanges();
        }

        
    }
}
