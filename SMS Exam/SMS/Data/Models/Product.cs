using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Range(0.05, 1000)]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Cart))]
        public string CardId { get; set; }

        public Cart Cart { get; set; }
    }
}
