using System;
using System.ComponentModel.DataAnnotations;


namespace SMS.Models
{
    public class CreateProductFormModel
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Name { get; set; }


        [Range(0.05, 1000, ErrorMessage = "{0} must be between {1} and {2}")]
        public decimal Price { get; set; }
    }
}
