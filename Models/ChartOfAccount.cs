using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trial.Server.Models
{
    [MongoCollection(nameof(DataBaseSettings.COACollectionName))]
    public class ChartOfAccount : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string accType { get; set; } = null!;
        public string accGroup { get; set; } = null!;

    }
}
