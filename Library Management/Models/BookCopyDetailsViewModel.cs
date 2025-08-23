using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class BookCopyDetailsViewModel
    {
        public Guid BookCopyId { get; set; }
        
        public Guid BookId { get; set; }

        [Display(Name = "Cover Image")]
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Condition")]
        public string? Condition { get; set; }

        [Display(Name = "Source")]
        public string? Source { get; set; }

        [Display(Name = "Added Date")]
        public DateTime? AddedDate { get; set; }

        [Display(Name = "Pullout Date")]
        public DateTime? PulloutDate { get; set; }

        [Display(Name = "Pullout Reason")]
        public string? PulloutReason { get; set; }

        [Display(Name = "Status")]
        public string Status => PulloutDate.HasValue ? "Pulled Out" : "Available";

        public bool IsPulledOut => PulloutDate.HasValue;
    }
}
