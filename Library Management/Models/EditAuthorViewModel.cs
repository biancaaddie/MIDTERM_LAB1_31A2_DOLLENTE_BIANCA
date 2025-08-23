using System.ComponentModel.DataAnnotations;

namespace Library_Management.Models
{
    public class EditAuthorViewModel
    {
        [Required(ErrorMessage = "Author ID is required.")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "Author name is required.")]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string? Name { get; set; }

        [Display(Name = "Biography")]
        [StringLength(2000, ErrorMessage = "Biography cannot exceed 2000 characters.")]
        public string? Biography { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Profile Image URL")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? ProfileImageUrl { get; set; }
        
        public bool IsArchived { get; set; } = false;
    }
}
