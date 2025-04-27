using System.ComponentModel.DataAnnotations;

namespace MovieReviewsApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }            // Foreign key to Movie

        [Range(1, 5)]
        public int Rating { get; set; }             // Rating 1-5

        public string Comment { get; set; }

        // Demographic fields
        public string AgeRange { get; set; }        // e.g. "18-25", "26-35", etc.
        public string Gender { get; set; }          // e.g. "Male", "Female", "Other"
        public string Continent { get; set; }       // e.g. "North America", "Europe"
        public string Country { get; set; }         // e.g. "USA", "Germany"

        // Navigation
        public Movie Movie { get; set; }
    }
}
