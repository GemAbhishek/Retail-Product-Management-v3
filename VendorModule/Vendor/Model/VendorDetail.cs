using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Model
{
    public class VendorDetail
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        [Range(0, Double.PositiveInfinity)]
        public double DeliveryCharge { get; set; }


        [Range(0, 5)]
        public int Rating { get; set; }
    }
}
