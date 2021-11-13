using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aggregator.API.Services;
using Aggregator.API.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Aggregator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IStockService _stockService;
        private readonly ILogger<CommonController> _logger; 
        private readonly IMapper mapper;

        public CommonController(ICompanyService companyService, IStockService stockService, ILogger<CommonController> logger, IMapper mapper)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{companyCode}", Name = "GetCompanyStocks")]
        [ProducesResponseType(typeof(CommonModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommonModel>> GetCompanyStocks(string companyCode)
        {
            var company = await _companyService.GetCompanyByCode(companyCode);

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;

            var stocks = await _stockService.GetStocksByDate(companyCode, fromDate.Date, toDate.Date);

            var commonModel = new CommonModel
            {
                CompanyCode = company.Code,
                CompanyDetails = company,
                StockDetails = stocks
            };

            return Ok(commonModel);
        }

        [HttpGet(Name = "GetLastestStockPrice")]
        [ProducesResponseType(typeof(CommonModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommonModel>> GetLastestStockPrice()
        {
            IEnumerable<CompanyModel> company = await _companyService.GetCompanyAll();
            IEnumerable<StockModel> stocks = await _stockService.GetLastestStockPrice();

            List<LatestStockPriceModel> allCompanies = mapper.Map<List<LatestStockPriceModel>>(company);

            List<StockModel> allStocks = stocks.ToList();
            allCompanies.ForEach(com => {
                var isExists = allStocks.Find(f => f.CompanyCode == com.Code);
                com.Price = isExists != null ? allStocks.SingleOrDefault(f => f.CompanyCode == com.Code).Price : 0;
            });

            return Ok(allCompanies);
        }
    }
}
