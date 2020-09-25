using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vendor.Model;

namespace Vendor.Repository
{
    public class VendorDetailRepo : IVendorDetailRepo
    {
        public static List<VendorDetail> vendors = new List<VendorDetail>
        {
            new VendorDetail{Id=1,Name="Cloud Retail",Rating=5,DeliveryCharge=80 },
            new VendorDetail{Id=2,Name="SSK Retail",Rating=5,DeliveryCharge=40 },
            new VendorDetail{Id=3,Name="ABB Retail",Rating=3,DeliveryCharge=60 },
            new VendorDetail{Id=4,Name="REL Retail",Rating=3,DeliveryCharge=70 },
            new VendorDetail{Id=5,Name="MFF Retail",Rating=5,DeliveryCharge=76 }
        };
        private IVendorDetailRepo @object;

        public VendorDetailRepo(IVendorDetailRepo @object)
        {
            this.@object = @object;
        }

        public List<VendorDetail> GetDetails()
        {
            return vendors;
        }

        public VendorDetail GetVenderbyId(int ProductId)
        {
            VenderStockRepo obj = new VenderStockRepo();
            List<int> VId = obj.GetVendorbyId(ProductId);
            foreach (VendorDetail v in vendors)
            {
                foreach (int i in VId)
                {
                    if (v.Id == i)
                    {
                        return v;
                    }
                }
            }
            return null;
        }

       
    }
}
