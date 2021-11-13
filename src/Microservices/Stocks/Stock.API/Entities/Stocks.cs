using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stock.API.Entities
{
    public class Stocks : BaseEntity
    {
        [BsonElement("CompanyCode")]
        public string CompanyCode { get; set; }
        public decimal Price { get; set; }
    }
}
