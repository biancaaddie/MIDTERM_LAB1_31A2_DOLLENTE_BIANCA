using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class AuthorListViewModel
    {
        public Guid AuthorId { get; set; }
        
        [Display(Name = "Name")]
        public string? Name { get; set; } = default!;
        
        [Display(Name = "Biography")]
        public string? Biography { get; set; } = default!;
        
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; } = default!;
        
        [Display(Name = "Profile Image")]
        public string? ProfileImageUrl { get; set; } = default!;
        
        [Display(Name = "Number of Books")]
        public int BookCount { get; set; } = 0;
        
        public bool IsArchived { get; set; } = false;
    }
}
