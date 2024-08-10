using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace media_player_backend.Models
{
    public class WatchHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VideoId { get; set; }
        public string Caption { get; set; }
        public string Url { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
