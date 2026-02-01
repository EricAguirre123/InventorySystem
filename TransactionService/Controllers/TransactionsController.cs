using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Services;
using TransactionService.Domain.Entities;

namespace TransactionService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly Application.Services.TransactionService _transactionService;

        public TransactionsController(
            Application.Services.TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionService.GetAllAsync();
            return Ok(result);
        }

        // ✅ GET CON FILTROS
        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? type,
            [FromQuery] int? productId
        )
        {
            var result = await _transactionService.GetFilteredAsync(
                from, to, type, productId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction transaction)
        {
            var result = await _transactionService.CreateAsync(transaction);
            return Ok(result);
        }

    }
}
