using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendor.Model;

namespace Vendor.Repository
{
    public class VenderStockRepo
    {
        public static List<VendorStock> vendorsproduct = new List<VendorStock>
        {
            new VendorStock{ProductId = 1, VendertId= 1, StockInHand = 50 , DeliveryDate = DateTime.Parse("12-10-2020")},
            new VendorStock{ProductId = 2, VendertId= 2, StockInHand = 50 , DeliveryDate = DateTime.Parse("13-10-2020")},
            new VendorStock{ProductId = 3, VendertId= 3, StockInHand = 50 , DeliveryDate = DateTime.Parse("14-10-2020")},
            new VendorStock{ProductId = 4, VendertId= 4, StockInHand = 50 , DeliveryDate = DateTime.Parse("15-10-2020")},
            new VendorStock{ProductId = 5, VendertId= 5, StockInHand = 50 , DeliveryDate = DateTime.Parse("16-10-2020")}

        };

       
        public List<int> GetVendorbyId(int ProductId)
        {
            List<int> ls = new List<int>();
            foreach(VendorStock v in vendorsproduct)
            {
                if(v.ProductId == ProductId && v.StockInHand > 1)
                {
                    ls.Add(v.VendertId);
                }
            }
            return ls;
        }
    }
}
