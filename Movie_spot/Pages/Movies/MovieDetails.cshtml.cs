using Microsoft.AspNetCore.Mvc.RazorPages;
using Movie_spot.Models; // Ensure this namespace matches where your Movie model is located  

namespace Movie_spot.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        public Movie Movie { get; set; }

        public void OnGet(int id)
        {
            // Fetch the movie details by id from your data source  
            // Example:  
            // Movie = _context.Movies.FirstOrDefault(m => m.Id == id);  
        }
    }
}
