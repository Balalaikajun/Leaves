using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Authentication(IConfiguration configuration)
        {
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                if(HttpContext.Request.Headers.Authorization.ToString() == "")
                {
                    return Unauthorized("Токен отсутсвует");
                }
                string token = HttpContext.Request.Headers.Authorization.ToString().Substring("Bearer ".Length);
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                // Добавляем полученный токен в ответ
                Response.Headers["Authorization"] = "Bearer " + token;
                return Ok("Токен действителен");
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized("Токен истек");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return Unauthorized("Неверная подпись токена");
            }
            
        }
    }
}
