using Acces.Context;
using Acces.Repositories;
using Data.Models;


namespace AuthMicroservice.Services
{
    public class UserSevice
    {
        private UserRepository _userRepository;

        public UserSevice(UserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public int AuthenticatesUser(string login, string password)
        {
            User user = _userRepository.GetUserByLogin(login);
            if (user == null)
            {
                throw new ArgumentException("Логин не существует");
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.Userpassword))
            {
                return user.PkUserid;
            }

            throw new UnauthorizedAccessException("Неверный пароль");

        }
    }
}
