using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserMicroservice.Services;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Authorize]
        [HttpPost]
        public ActionResult CreateAccount( AccountService accountService)
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var idClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id))
            {
                // Теперь id содержит значение ID пользователя
                accountService.Create(id);
                return Ok("Счёт создан");
            }
            else
            {
                return BadRequest("Неверный Id");
            }
        }

        [Authorize]
        [HttpGet("{accountId}")]
        public ActionResult<Account> GetAccountInfo(int accountId, AccountService accountService)
        {
            return Ok(accountService.Get(accountId));

        }

        [Authorize]
        [HttpGet("accounts")]
        public ActionResult<Account[]> GetAccountsInfo(AccountService accountService)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var idClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id))
            {
                // Теперь id содержит значение ID пользователя
                return Ok(accountService.GetAccounts(id).ToArray());
            }
            else
            {
                return BadRequest("Неверный Id");
            }


        }


    }
}
