using System.ComponentModel.DataAnnotations;

namespace RestaurantReviews.WebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        // the HTML/tag helpers like "DisplayNameFor"
        // will use this instead of the property's name
        [Display(Name = "Reviewer Name")]
        public string ReviewerName { get; set; }
        public int? Score { get; set; }
        public string Text { get; set; }
    }
}