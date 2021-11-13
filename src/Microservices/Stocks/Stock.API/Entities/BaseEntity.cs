using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Stock.API.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CreatedBy")]
        public string CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedDate { get; set; }

        [BsonElement("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }

    }
}
