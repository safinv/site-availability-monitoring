using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SiteAvailabilityMonitoring.Domain.Models
{
    public class BaseDbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}