// Services/StatisticsService.cs
using Microsoft.EntityFrameworkCore;
using MovieReviewsApp.Data;
using MovieReviewsApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_spot.Services
{
    public class StatisticsService
    {
        private readonly ApplicationDbContext _db;

        public StatisticsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Dictionary<string, int>> GetMovieCountByGenreAsync()
        {
            return await _db.Movies
                .GroupBy(m => m.Genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Genre, x => x.Count);
        }

        public async Task<Dictionary<int, int>> GetMovieCountByYearAsync()
        {
            return await _db.Movies
                .GroupBy(m => m.ReleaseYear)
                .Select(g => new { Year = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Year, x => x.Count);
        }

        public async Task<double> GetAverageRatingAsync()
        {
            return await _db.Reviews.AverageAsync(r => r.Rating);
        }

        public async Task<List<MovieWithRating>> GetTopRatedMoviesAsync(int count = 5)
        {
            return await _db.Movies
                .Include(m => m.Reviews)
                .Where(m => m.Reviews.Any())
                .Select(m => new MovieWithRating
                {
                    Id = m.Id,
                    Title = m.Title,
                    AverageRating = m.Reviews.Average(r => r.Rating)
                })
                .OrderByDescending(m => m.AverageRating)
                .Take(count)
                .ToListAsync();
        }
    }

    public class MovieWithRating
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double AverageRating { get; set; }
    }
}
