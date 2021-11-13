using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stock.API.Entities;
using Stock.API.Model;

namespace Stock.API.Repositories
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stocks>> GetStocks();
        Task<IEnumerable<Stocks>> GetStocksByDate(string companyCode, DateTime fromDate, DateTime toDate);

        Task<IEnumerable<CurrentStockPriceVm>> GetLastestStockPrice();

        Task<IEnumerable<Stocks>> AddStock(Stocks stock);
        Task<bool> DeleteStockByCode(string companyCode);
        Task<IEnumerable<Stocks>> DeleteStock(SearchModel stock);
    }
}
