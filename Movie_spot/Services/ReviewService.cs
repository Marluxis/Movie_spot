using System.Linq;
using MovieReviewsApp.Data;
using MovieReviewsApp.Models;

namespace MovieReviewsApp.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _db;
        public ReviewService(ApplicationDbContext db) { _db = db; }

        public void AddReview(int movieId, Review review)
        {
            // Ensure the review is linked to the movie
            review.MovieId = movieId;
            _db.Reviews.Add(review);
            _db.SaveChanges();
        }

        public MovieRatingStats GetAverageRatingsByDemographics(int movieId)
        {
            // Get all reviews for the movie
            var reviews = _db.Reviews.Where(r => r.MovieId == movieId).ToList();

            // Calculate average rating grouped by age range, gender, and continent
            var avgByAge = reviews
                .GroupBy(r => r.AgeRange)
                .Select(g => new { AgeRange = g.Key, AvgRating = g.Average(r => r.Rating) })
                .ToList();

            var avgByGender = reviews
                .GroupBy(r => r.Gender)
                .Select(g => new { Gender = g.Key, AvgRating = g.Average(r => r.Rating) })
                .ToList();

            var avgByContinent = reviews
                .GroupBy(r => r.Continent)
                .Select(g => new { Continent = g.Key, AvgRating = g.Average(r => r.Rating) })
                .ToList();

            // Wrap results in a DTO class (defined below)
            return new MovieRatingStats
            {
                ByAgeRange = avgByAge,
                ByGender = avgByGender,
                ByContinent = avgByContinent
            };
        }
    }

    // DTO for returning grouped average ratings
    public class MovieRatingStats
    {
        public object ByAgeRange { get; set; }
        public object ByGender { get; set; }
        public object ByContinent { get; set; }
    }
}
