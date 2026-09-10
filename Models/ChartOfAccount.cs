using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trial.Server.Models
{
    public class ChartOfAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
