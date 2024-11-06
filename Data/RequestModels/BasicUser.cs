namespace Data.RequestModels
{
    public class BasicUser
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public BasicUser(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
