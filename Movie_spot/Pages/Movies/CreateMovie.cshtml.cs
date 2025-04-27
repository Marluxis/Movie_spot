using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Movie_spot.Pages.Movies
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public required MovieInputModel Movie { get; set; }

        [BindProperty]
        public required IFormFileCollection ImageFiles { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Handle saving the movie and images here

            return RedirectToPage("./Index");
        }

        public class MovieInputModel
        {
            [Required]
            [StringLength(100)]
            public string Title { get; set; }

            [Required]
            [StringLength(100)]
            public string Director { get; set; }

            [Required]
            [StringLength(200)]
            public string Artists { get; set; }

            [Required]
            [StringLength(50)]
            public string Genre { get; set; }

            [Required]
            [Range(1900, 2100)]
            public int ReleaseYear { get; set; }
        }
    }
}
