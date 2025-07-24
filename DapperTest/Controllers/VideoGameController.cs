using DapperTest.Models;
using DapperTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperTest.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class VideoGamesController : ControllerBase
        {
            private readonly IVideoGameRepository _videoGameRepository;

            public VideoGamesController(IVideoGameRepository videoGameRepository)
            {
                _videoGameRepository = videoGameRepository;
            }

            [HttpGet]
            public async Task<ActionResult<List<VideoGame>>> GetAll()
            {
                var videoGames = await _videoGameRepository.GetAllAsync();
                return Ok(videoGames);
            }

            [HttpGet("{id}",Name ="GetById")]
            public async Task<ActionResult<VideoGame>> GetByIdAsync(int id)
            {
                var videoGame = await _videoGameRepository.GetByIdAsync(id);
                return videoGame;
            }

            [HttpPost]
            public async Task CreateAsync(VideoGame v1)
            {
                await _videoGameRepository.CreateAsync(v1);
            }

            [HttpDelete("{id}")]
            public async Task DeleteAsync(int id)
            {
                await _videoGameRepository.DeleteAsync(id);
            }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, VideoGame videoGame)
        {
            var existingGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingGame == null)
                return NotFound("Video Game not found.");

            videoGame.Id = id;
            await _videoGameRepository.UpdateAsync(videoGame);
            return Ok();
        }

    }
    }
