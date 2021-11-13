using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Stock.API.Data;
using Stock.API.Entities;
using Stock.API.Model;

namespace Stock.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IStockContext _dbContext;
        private readonly ILogger<StockRepository> _logger;

        public StockRepository(IStockContext dbContext, ILogger<StockRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Stocks>> GetStocks()
        {
            var allStocks = await _dbContext.Stocks.Find(f => true).SortByDescending(s => s.CreatedDate).ToListAsync();
            return allStocks;//.Count > 0 ? allStocks : null;
        }

        public async Task<IEnumerable<Stocks>> GetStocksByDate(string companyCode, DateTime fromDate, DateTime toDate)
        {
            fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 00, 00, 00);
            toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59);

            FilterDefinition<Stocks> filter = Builders<Stocks>.Filter.Eq(p => p.CompanyCode, companyCode);
            filter &= Builders<Stocks>.Filter.Gte(f => f.CreatedDate, fromDate);
            filter &= Builders<Stocks>.Filter.Lte(f => f.CreatedDate, toDate);

            var filteredStocks = await _dbContext.Stocks.Find(filter).SortByDescending(s => s.CreatedDate).ToListAsync();
            //return filteredStocks.Count > 0 ? filteredStocks : null;
            return filteredStocks;
        }

        public async Task<IEnumerable<Stocks>> AddStock(Stocks stock)
        {
            stock.CreatedDate = DateTime.UtcNow;
            await _dbContext.Stocks.InsertOneAsync(stock);

            DateTime fromDate = stock.CreatedDate.AddDays(-30);
            DateTime toDate = stock.CreatedDate;
            return await GetStocksByDate(stock.CompanyCode, fromDate.Date, toDate.Date);
        }

        public async Task<bool> DeleteStockByCode(string companyCode)
        {
            FilterDefinition<Stocks> filter = Builders<Stocks>.Filter.Eq(p => p.CompanyCode, companyCode);

            DeleteResult deleteResult = await _dbContext.Stocks.DeleteManyAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Stocks>> DeleteStock(SearchModel searchModel)
        {
            FilterDefinition<Stocks> filter = Builders<Stocks>.Filter.Eq(p => p.Id, searchModel.Id);
            DeleteResult deleteResult = await _dbContext.Stocks.DeleteOneAsync(filter);

            DateTime fromDate = DateTime.UtcNow.AddDays(-30);
            DateTime toDate = DateTime.UtcNow;

            if (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0)
            {
                fromDate = searchModel.FromDate;
                toDate = searchModel.ToDate;   
                
                return await GetStocksByDate(searchModel.CompanyCode, fromDate.Date, toDate.Date);
            }

            return await GetStocksByDate(searchModel.CompanyCode, fromDate.Date, toDate.Date);
            //return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<CurrentStockPriceVm>> GetLastestStockPrice()
        {
            return await _dbContext.Stocks.Aggregate()
                      .Sort(Builders<Stocks>.Sort.Descending("CreatedDate"))
                      .Group(y => y.CompanyCode,
                             z => new CurrentStockPriceVm
                             {
                                 CompanyCode = z.Key,
                                 //LatestStockPrice = z.OrderByDescending(o => o.CreatedDate).FirstOrDefault().Price
                                 Price = z.First().Price
                             }
                      ).ToListAsync();
        }

        public DateTime ConvertUSTtoISTDateTime(DateTime? dateValue = null)
        {
            DateTime timeUtc = dateValue ?? DateTime.UtcNow;
            try
            {
                TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, istZone);
                return istTime;
                //Console.WriteLine("The date and time are {0} {1}.",
                //                  istTime,
                //                  istZone.IsDaylightSavingTime(istTime) ?
                //                          istZone.DaylightName : istZone.StandardName);
            }
            catch (TimeZoneNotFoundException)
            {
                _logger.LogError("The registry does not define the India Standard Time zone.");
                return timeUtc;
            }
            catch (InvalidTimeZoneException)
            {
                _logger.LogError("Registry data on the India Standard Time zone has been corrupted.");
                return timeUtc;
            }
        }
    }
}
