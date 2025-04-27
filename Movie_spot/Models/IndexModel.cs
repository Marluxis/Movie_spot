// Pages/Statistics/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie_spot.Services;

namespace Movie_spot.Models
{
    public class IndexModel : PageModel
    {
        private readonly StatisticsService _statisticsService;

        public IndexModel(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public Dictionary<string, int> GenreDistribution { get; set; }
        public Dictionary<int, int> YearDistribution { get; set; }
        public double AverageRating { get; set; }
        public List<MovieWithRating> TopRatedMovies { get; set; }

        public async Task OnGetAsync()
        {
            GenreDistribution = await _statisticsService.GetMovieCountByGenreAsync();
            YearDistribution = await _statisticsService.GetMovieCountByYearAsync();
            AverageRating = await _statisticsService.GetAverageRatingAsync();
            TopRatedMovies = await _statisticsService.GetTopRatedMoviesAsync();
        }
    }
}
