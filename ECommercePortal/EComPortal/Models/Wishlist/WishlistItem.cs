using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComPortal.Models.Wishlist
{
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
