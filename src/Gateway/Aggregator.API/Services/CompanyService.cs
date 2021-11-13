using Aggregator.API.Extensions;
using Aggregator.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _client;

        public CompanyService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanyAll()
        {
            var url = $"/api/Company/";
            var response = await _client.GetAsync(url);
            return await response.ReadContentAs<List<CompanyModel>>();
        }

        public async Task<CompanyModel> GetCompanyByCode(string code)
        {
            //var url = "http://companys.api/api/v1.0/market/company/info/A001";
            //var url = $"/api/Company/{code}";
            var url = $"/api/Company/{code}";
            var response = await _client.GetAsync(url);
            //var response = await _client.GetAsync($"/api/v1.0/market/company/info/{code}");
            return await response.ReadContentAs<CompanyModel>();
        }
    }
}
