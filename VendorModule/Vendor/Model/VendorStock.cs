using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vendor.Model
{
    public class VendorStock
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int VendertId { get; set; }

        [Required]
        public int StockInHand { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
