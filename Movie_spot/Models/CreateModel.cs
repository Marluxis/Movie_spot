// Pages/Movies/Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieReviewsApp.Models;
using MovieReviewsApp.Services;

namespace Movie_spot.Models
{
    public class CreateModel : PageModel
    {
        private readonly MovieService _movieService;

        public CreateModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public List<IFormFile> ImageFiles { get; set; }

        public void OnGet()
        {
            // Initialize a new movie object
            Movie = new Movie();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _movieService.AddMovieAsync(Movie, ImageFiles);
            
            return RedirectToPage("./Index");
        }
    }
}
