using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aggregator.API.Extensions;
using Aggregator.API.Models;

namespace Aggregator.API.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;

        public StockService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<StockModel>> GetStocksByDate(string companyCode, DateTime fromDate, DateTime toDate)
        {
            var url = $"/api/Stock/{companyCode}/{fromDate.ToString("yyyy-MM-dd")}/{toDate.ToString("yyyy-MM-dd")}";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAs<List<StockModel>>();
        }

        public async Task<IEnumerable<StockModel>> GetLastestStockPrice()
        {
            var url = $"/api/Stock/GetLastestStockPrice";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAs<List<StockModel>>();
        }
    }
}
