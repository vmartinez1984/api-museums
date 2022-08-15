using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Museums.Core.Entities;

public class CrontabEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public int Minute { get; set; }
    
    public int Hour { get; set; }
    
    public int DayOfMonth { get; set; }
    
    public int Month { get; set; }

    public int DayOfWeek { get; set; }

    public bool IsActivate { get; set; } = true;
}