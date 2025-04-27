using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MovieReviewsApp.Data;
using MovieReviewsApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewsApp.Services
{
    public class MovieService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MovieService> _logger;

        public MovieService(ApplicationDbContext db, IWebHostEnvironment env, ILogger<MovieService> logger)
        {
            _db = db;
            _env = env;
            _logger = logger;
        }

        public async Task AddMovieAsync(Movie movie, IList<IFormFile> images)
        {
            try
            {
                // Save movie first (to generate an Id)
                await _db.Movies.AddAsync(movie);
                await _db.SaveChangesAsync();

                // Handle image uploads, if any
                if (images != null && images.Count > 0)
                {
                    string wwwRootPath = _env.WebRootPath; // path to wwwroot
                    foreach (var file in images)
                    {
                        if (file.Length > 0)
                        {
                            // Generate unique file name and save to wwwroot/images
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string filePath = Path.Combine(wwwRootPath, "images", fileName);

                            // Save file to disk
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // Create a MovieImage record linked to the movie
                            var movieImage = new MovieImage { MovieId = movie.Id, ImagePath = fileName };
                            await _db.MovieImages.AddAsync(movieImage);
                        }
                    }
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a movie.");
                throw;
            }
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            try
            {
                // Eager-load Images and Reviews so the UI can show them without extra queries
                return await _db.Movies
                                .Include(m => m.Images)
                                .Include(m => m.Reviews)
                                .OrderBy(m => m.Title)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all movies.");
                throw;
            }
        }

        public async Task<Movie> GetMovieWithDetailsAsync(int movieId)
        {
            try
            {
                // Retrieve movie with related images and reviews from database
                return await _db.Movies
                                .Include(m => m.Images)
                                .Include(m => m.Reviews)
                                .FirstOrDefaultAsync(m => m.Id == movieId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving movie with ID {movieId}.");
                throw;
            }
        }
    }
}
