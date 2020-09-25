using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComPortal.Models.Cart
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        public double Price { get; set; }
        public string Description { get; set; }

        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        public double DeliveryCharge { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
