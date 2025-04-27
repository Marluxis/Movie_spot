using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movie_spot.Pages.Movies
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Movie Movie { get; set; }

        [BindProperty]
        public List<IFormFile> ImageFiles { get; set; }

        public void OnGet(int id)
        {
            // Fetch the movie by ID from the database or repository  
            Movie = GetMovieById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update the movie in the database or repository  
            UpdateMovie(Movie, ImageFiles);

            return RedirectToPage("./Index");
        }

        private Movie GetMovieById(int id)
        {
            // Mock implementation for fetching a movie  
            return new Movie
            {
                Id = id,
                Title = "Sample Movie",
                Director = "Sample Director",
                Artists = "Actor1, Actor2",
                Genre = "Drama",
                ReleaseYear = 2022,
                Images = new List<MovieImage>
               {
                   new MovieImage { ImagePath = "sample1.jpg" },
                   new MovieImage { ImagePath = "sample2.jpg" }
               }
            };
        }

        private void UpdateMovie(Movie movie, List<IFormFile> imageFiles)
        {
            // Mock implementation for updating a movie  
            // Save the movie and handle image uploads  
        }
    }

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }

        public string Artists { get; set; }

        [Required]
        public string Genre { get; set; }

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        public List<MovieImage> Images { get; set; }
    }

    public class MovieImage
    {
        public string ImagePath { get; set; }
    }
}
