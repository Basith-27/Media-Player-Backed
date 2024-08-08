using media_player_backend.Models;
using media_player_backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace media_player_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly MediaPlayerContext _context;

        public VideosController(MediaPlayerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            var videos = await _context.Videos.Find(v => true).ToListAsync();
            return Ok(videos);
        }

        [HttpPost]
        public async Task<ActionResult<Video>> UploadVideo([FromBody] Video video)
        {
            video.MongoId = ObjectId.GenerateNewId();
            await _context.Videos.InsertOneAsync(video);
            return CreatedAtAction(nameof(GetVideoById), new { id = video.Id }, video);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideoById(string id)
        {
            var video = await _context.Videos.Find(v => v.Id == id).FirstOrDefaultAsync();
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(string id)
        {
            var result = await _context.Videos.DeleteOneAsync(v => v.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
