using Movie_spot.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsApp.Models
{
    public class Movie
    {
        public int Id { get; set; }  // Primary key

        [Required]
        public string Title { get; set; }

        public string Artists { get; set; }    // e.g. lead actors
        public string Director { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }

        // Navigation properties
        public List<MovieImage> Images { get; set; } = new List<MovieImage>();
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}