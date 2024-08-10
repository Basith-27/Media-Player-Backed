using media_player_backend.Models;
using media_player_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace media_player_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchHistoryController : ControllerBase
    {
        private readonly IMongoCollection<WatchHistory> _watchHistories;

        public WatchHistoryController(MediaPlayerContext context)
        {
            _watchHistories = context.WatchHistories;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchHistory>>> GetWatchHistory()
        {
            var watchHistory = await _watchHistories.Find(_ => true).ToListAsync();
            return Ok(watchHistory);
        }

        // POST: api/watchhistory
        [HttpPost]
        public async Task<ActionResult<WatchHistory>> AddWatchHistory(WatchHistory watchHistory)
        {
            watchHistory.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            watchHistory.Timestamp = DateTime.UtcNow;
            await _watchHistories.InsertOneAsync(watchHistory);
            return CreatedAtAction(nameof(GetWatchHistory), new { id = watchHistory.Id }, watchHistory);
        }

        // DELETE: api/watchhistory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchHistory(string id)
        {
            var result = await _watchHistories.DeleteOneAsync(w => w.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
