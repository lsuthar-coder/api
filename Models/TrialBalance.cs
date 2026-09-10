using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Trial.Server.Models
{
    public class TrialBalance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public TBEntry[] Entries { get; set; }
    }

    public class TBEntry
    {
        public string Name { get; set; } = null!;
        [BsonElement("Code")]
        public string AccountCode { get; set; } = null!;
        public float Debit { get; set; }
        public float Credit { get; set; }
    }
}
