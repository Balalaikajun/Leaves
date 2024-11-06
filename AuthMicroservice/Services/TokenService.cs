using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration.Json;
using System.Text;
using System.Security.Claims;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.DataProtection;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;


namespace AuthMicroservice.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(int userID)
        {
            //Создание секрета
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"]));
            SigningCredentials creditials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Добавление содержимого в токен
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userID.ToString())
            };

            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creditials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       


        //Изменение секрета сервера
        public void UpdateSecret(string secret)
        {
            //Подготовка appsettings для редактирования
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            JObject jsonObj = JObject.Parse(File.ReadAllText(path));


            //Создание постоянного токена
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials creditials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddYears(100),
                signingCredentials: creditials);


            jsonObj["Token"]["Secret"] = secret;
            jsonObj["Token"]["ValidToken"] = new JwtSecurityTokenHandler().WriteToken(token);

            File.WriteAllText(path, jsonObj.ToString());
        }
    }
}
