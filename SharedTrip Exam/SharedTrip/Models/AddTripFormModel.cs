using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class AddTripFormModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} can't be null or whitespace")]
        public string StartPoint { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} can't be null or whitespace")]
        public string EndPoint { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} can't be null or whitespace")]
        public string DepartureTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} can't be null or whitespace")]
        [Range(2, 6)]
        public int Seats { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} can't be null or whitespace")]
        [MaxLength(80, ErrorMessage = "{0} cant be over 80 characters")]
        public string Description { get; set; }

        public string ImagePath { get; set; }


    }
}
