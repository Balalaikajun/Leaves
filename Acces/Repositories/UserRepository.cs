using Acces.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acces.Repositories
{
    public class UserRepository
    {
        private LeavesDbContext _leavesContext;

        public UserRepository(LeavesDbContext leavesContext)
        {
            _leavesContext = leavesContext;
        }

        public void CreateUser(string login, string password)
        {
            _leavesContext.Users.Add(new User
            {
                UqLogin = login,
                Userpassword = password
            });
        }

        public User GetUserById(int id) 
        {
            return _leavesContext.Users.SingleOrDefault(x => x.PkUserid == id);
        }
        public User GetUserByLogin(string login)
        {
            return _leavesContext.Users.SingleOrDefault(x => x.UqLogin == login);
        }
        public void DeleteUser(int id) 
        {
            _leavesContext.Users.Remove(GetUserById(id));
        }
        
    }
}
