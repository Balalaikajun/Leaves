using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserMicroservice.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [Authorize]
        [HttpGet("auth")]
        public IActionResult GetWithAuth()
        {
            return Ok("Урааа!");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ура!");
        }
    }
}
