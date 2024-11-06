using Data.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionMicroservice.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionMicroservice.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        [Authorize]
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionDTO transactionDTO, TransactionService transactionService)
        {
            try
            {
                transactionService.CreateTransaction(transactionDTO.AccountFromId, transactionDTO.AccountToId, transactionDTO.Amount);
                return Ok("Транзакция проведена успешно");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
