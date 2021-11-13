using Aggregator.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.API.Services
{
    public interface IStockService
    {
        Task<IEnumerable<StockModel>> GetStocksByDate(string companyCode, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<StockModel>> GetLastestStockPrice();
    }
}
