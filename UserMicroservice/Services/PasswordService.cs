namespace UserMicroservice.Services
{
    public class PasswordService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool Verify(string password,string encrypdetPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, encrypdetPassword);
        }
    }
}
