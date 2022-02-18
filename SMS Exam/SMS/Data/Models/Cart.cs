using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            this.Products = new HashSet<Product>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
    
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
