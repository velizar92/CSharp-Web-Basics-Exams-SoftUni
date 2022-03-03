using System.ComponentModel.DataAnnotations;

namespace FootballManager.ViewModels
{
    public class AddPlayerFormModel
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        public byte Speed { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        public byte Endurance { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Description { get; set; }

    }
}
