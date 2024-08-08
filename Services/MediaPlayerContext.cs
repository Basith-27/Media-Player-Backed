using media_player_backend.Configurations;
using media_player_backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace media_player_backend.Services
{
    public class MediaPlayerContext
    {
        private readonly IMongoDatabase _database;

        public MediaPlayerContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Video> Videos => _database.GetCollection<Video>("Videos");
    }
}
