using MongoDB.Driver;
using Stock.API.Entities;

namespace Stock.API.Data
{
    public interface IStockContext
    {
        IMongoCollection<Stocks> Stocks { get; }
    }
}
