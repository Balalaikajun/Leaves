using AuthMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Data.RequestModels;



namespace AuthMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        [HttpPost]
        public IActionResult Authorize([FromBody] BasicUser user, UserSevice userSevice, TokenService tokenService)
        {
            try
            {
                int userID = userSevice.AuthenticatesUser(user.Login, user.Password);
                return Ok( tokenService.GenerateJwtToken(userID));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
    }
}
