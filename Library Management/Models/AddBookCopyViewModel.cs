using System;
using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class AddBookCopyViewModel
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid BookId { get; set; }  // Which book this copy belongs to

        [Required(ErrorMessage = "Cover image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string CoverImageUrl { get; set; }  // URL of cover image

        [Required(ErrorMessage = "Condition is required.")]
        public string Condition { get; set; }  // e.g., "New", "Good", "Damaged"

        [Required(ErrorMessage = "Source is required.")]
        public string Source { get; set; }  // e.g., "Purchase", "Donation"
    }
}
