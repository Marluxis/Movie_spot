using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieReviewsApp.Services;

namespace MovieReviewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsApiController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        public StatsApiController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: /api/StatsApi?movieId=5
        [HttpGet]
        public IActionResult GetMovieStats(int movieId)
        {
            var stats = _reviewService.GetAverageRatingsByDemographics(movieId);
            return Ok(stats);
        }
    }
}
