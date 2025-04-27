using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieReviewsApp.Models
{
    public class MovieImage
    {
        public int Id { get; set; }
        public int MovieId { get; set; }             // Foreign key to Movie

        [Required]
        public string ImagePath { get; set; }        // Relative path or filename of the image

        // Navigation property
        public Movie Movie { get; set; }
    }
}
