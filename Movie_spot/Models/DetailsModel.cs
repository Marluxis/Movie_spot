// Pages/Movies/Details.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewsApp.Models;
using MovieReviewsApp.Services;

namespace Movie_spot.Models
{
    public class DetailsModel : PageModel
    {
        private readonly MovieService _movieService;
        private readonly ReviewService _reviewService;

        public DetailsModel(MovieService movieService, ReviewService reviewService)
        {
            _movieService = movieService;
            _reviewService = reviewService;
        }

        public Movie Movie { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _movieService.GetMovieWithDetailsAsync(id);
            
            if (Movie == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
