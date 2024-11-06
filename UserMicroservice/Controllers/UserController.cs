using Microsoft.AspNetCore.Mvc;
using Data.Models;
using UserMicroservice.Services;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Data.RequestModels;
using Acces.Context;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult CreateUser([FromBody] BasicUser request,  UserService userService)
        {
            try
            {
                userService.CreateUser(request.Login, request.Password);
                return Ok("Пользователь создан");
            }
            catch (DuplicateNameException)
            {
                return BadRequest("Данный логин уже существует");
            }
            
        }

        

        

    }
}
