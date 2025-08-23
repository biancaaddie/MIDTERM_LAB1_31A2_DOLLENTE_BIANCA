using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class PulloutBookCopyViewModel
    {
        [Required(ErrorMessage = "Book Copy ID is required.")]
        public Guid BookCopyId { get; set; }

        public Guid BookId { get; set; }

        [Display(Name = "Book Title")]
        public string? BookTitle { get; set; }

        [Display(Name = "Cover Image")]
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Condition")]
        public string? Condition { get; set; }

        [Display(Name = "Source")]
        public string? Source { get; set; }

        [Required(ErrorMessage = "Pullout reason is required.")]
        [Display(Name = "Reason for Pullout")]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters.")]
        public string? PulloutReason { get; set; }

        [Display(Name = "Pullout Date")]
        public DateTime PulloutDate { get; set; } = DateTime.Now;
    }
}
