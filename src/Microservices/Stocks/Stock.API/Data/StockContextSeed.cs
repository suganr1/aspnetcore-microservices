using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Stock.API.Entities;

namespace Stock.API.Data
{
    public class StockContextSeed
    {
        public static void SeedData(IMongoCollection<Stocks> stockCollection)
        {
            bool stockExists = stockCollection.Find(f => true).Any();
            if (!stockExists)
            {
                stockCollection.InsertManyAsync(GetDefaultStockList());
            }
        }

        private static IEnumerable<Stocks> GetDefaultStockList()
        {
            return new List<Stocks>()
            {
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757578",
                    CompanyCode = "A020",
                    Price = Convert.ToDecimal("98.85"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757579",
                    CompanyCode = "A020",
                    Price = Convert.ToDecimal("100.5"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddMinutes(-30)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757580",
                    CompanyCode = "A020",
                    Price = Convert.ToDecimal("99.30"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddDays(-1)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757581",
                    CompanyCode = "A020",
                    Price = Convert.ToDecimal("90.68"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddDays(-1).AddMinutes(-20)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757582",
                    CompanyCode = "A020",
                    Price = Convert.ToDecimal("95.47"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddMinutes(-10)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757583",
                    CompanyCode = "A019",
                    Price = Convert.ToDecimal("99.30"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddDays(-2).AddMinutes(-1)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757584",
                    CompanyCode = "A019",
                    Price = Convert.ToDecimal("90.68"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddDays(-2).AddMinutes(-10)
                },
                new Stocks()
                {
                    Id = "666f6f2d6261722d71757585",
                    CompanyCode = "A019",
                    Price = Convert.ToDecimal("95.47"),
                    CreatedBy = "SUGAN",
                    CreatedDate = DateTime.UtcNow.AddDays(-2).AddMinutes(-40)
                }
            };
        }
    }
}
