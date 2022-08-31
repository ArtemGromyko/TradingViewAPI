using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TradingView.DAL.Entities;

public class EntityBase
{
    [JsonIgnore]
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? EntityId { get; set; }
}
