using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace media_player_backend.Models
{
    public class Video
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MongoId { get; set; }

        public string Id { get; set; }
        public string Caption { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}
