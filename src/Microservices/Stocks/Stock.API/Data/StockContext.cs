using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Stock.API.Entities;

namespace Stock.API.Data
{
    public class StockContext : IStockContext
    {
        public StockContext(IConfiguration configuration)
        {
            var server = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = server.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Stocks = database.GetCollection<Stocks>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            StockContextSeed.SeedData(Stocks);
        }

        public IMongoCollection<Stocks> Stocks { get; }
    }
}
