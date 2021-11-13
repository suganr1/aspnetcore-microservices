using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.API.Entities;
using Stock.API.Repositories;
using Stock.API.Model;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockRepository stockRepository, ILogger<StockController> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Stocks>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Stocks>>> GetStocks()
        {
            var allStocks = await _stockRepository.GetStocks();

            if (allStocks == null)
            {
                _logger.LogError($"No Stocks Retrieved.");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved {allStocks.Count()} Stocks.");
            return Ok(allStocks);
        }

        [Route("{companyCode}/{fromDate}/{toDate}", Name = "GetStocksByDate")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Stocks>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Stocks>>> GetStocksByDate(string companyCode, DateTime fromDate, DateTime toDate)
        {
            var filteredStocks = await _stockRepository.GetStocksByDate(companyCode, fromDate, toDate);
            var message = $"For the date range { fromDate:MM/dd/yyyy HH:mm:ss} - {toDate:MM/dd/yyyy HH:mm:ss}.";

            if (filteredStocks == null)
            {
                _logger.LogError($"No Stocks Retrieved. {message}");
                return NotFound();
            }

            _logger.LogInformation($"Retrieved {filteredStocks.Count()} Stocks. {message}");
            return Ok(filteredStocks);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Stocks>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<Stocks>>> AddStock([FromBody] Stocks stock)
        {
            if (stock is null)
                return BadRequest(new ArgumentNullException());

            //stocks.CreatedDate = DateTime.Now.ToUniversalTime().ToLocalTime();
            // await _stockRepository.AddStock(stock);
            var filteredStocks = await _stockRepository.AddStock(stock);
            _logger.LogInformation($"Stock Created Successfully. {stock.CompanyCode} - {stock.Price}.");

            return Ok(filteredStocks);
        }

        [HttpDelete("{companyCode}", Name = "DeleteStockByCode")]
        [ProducesResponseType(typeof(Stocks), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteStockByCode(string companyCode)
        {
            await _stockRepository.DeleteStockByCode(companyCode);

            _logger.LogInformation($"All Stocks Deleted Successfully. {companyCode}.");
            return Ok();
        }


        [HttpDelete(Name = "DeleteStock")]
        [ProducesResponseType(typeof(Stocks), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteStockById([FromBody] SearchModel searchModel)
        {
            //await _stockRepository.DeleteStock(searchModel);

            var filteredStocks = await _stockRepository.DeleteStock(searchModel);

            _logger.LogInformation($"Stock Deleted Successfully.");
            return Ok(filteredStocks);
        }


        [HttpGet("GetLastestStockPrice", Name = "GetLastestStockPrice")]
        [ProducesResponseType(typeof(IEnumerable<CurrentStockPriceVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CurrentStockPriceVm>>> GetLastestStockPrice()
        {
            var latestStockPrice = await _stockRepository.GetLastestStockPrice();

            if (latestStockPrice == null)
            {
                return NotFound();
            }

            return Ok(latestStockPrice);
        }
    }
}
