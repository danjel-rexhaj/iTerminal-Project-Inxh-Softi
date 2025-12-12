using TerminalLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerminalLibrary.Models;

namespace iTerminal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrappingController : ControllerBase
    {
        private readonly WebScrappingService _webScrappingService;

        public ScrappingController(WebScrappingService webScrappingService)
        {
            _webScrappingService = webScrappingService;
        }

        [HttpGet("exchange-rate")]
        public async Task<IActionResult> GetEuroToLekExchangeRate()
        {
            try
            {
                var exchangeRate = await _webScrappingService.GetEuroToLekExchangeRateAsync();
                return Ok(new { ExchangeRate = exchangeRate });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
