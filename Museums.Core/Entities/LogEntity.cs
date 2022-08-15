using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Museums.Core.Entities
{
    public class LogEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime DateExecution { get; set; } = DateTime.Now;

        public int NumberOfUpdates { get; set; } = 0;

        public int NumberErrors { get; set; } = 0;

        public string MuseumIdInProcess { get; set; }

        public int TotalRecords { get; set; } = 0;

        public DateTime? DateCancelation { get; set; }

        public DateTime? DateEndExecution { get; set; }
    }
}