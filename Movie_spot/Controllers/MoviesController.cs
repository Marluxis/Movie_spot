using Microsoft.AspNetCore.Mvc;
using MovieReviewsApp.Models;
using MovieReviewsApp.Services;

namespace MovieReviewsApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieService _movieService;
        private readonly ReviewService _reviewService;
        public MoviesController(MovieService movieService, ReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        // GET: /Movies (or /Movies/Index)
        public async Task<IActionResult> Index()
        {
            var allMovies = await _movieService.GetAllMoviesAsync();  // Updated to use async method
            return View(allMovies);
        }

        // GET: /Movies/Create
        public IActionResult Create()
        {
            return View();  // return empty form
        }

        // POST: /Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, IList<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddMovieAsync(movie, images);  // Updated to use async method
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: /Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieWithDetailsAsync(id);  // Updated to use async method
            if (movie == null)
            {
                return NotFound();
            }
            // Optionally, we could load stats here and pass to viewmodel
            return View(movie);
        }

        // POST: /Movies/AddReview/5  (called from Details page form via AJAX or form post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(int movieId, Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewService.AddReview(movieId, review);
            }
            // After adding, redirect back to the Details page anchor for reviews
            return RedirectToAction(nameof(Details), new { id = movieId });
        }
    }
}