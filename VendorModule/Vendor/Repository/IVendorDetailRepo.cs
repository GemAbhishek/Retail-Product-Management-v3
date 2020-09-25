using System.Collections.Generic;
using Vendor.Model;

namespace Vendor.Repository
{
    public interface IVendorDetailRepo
    {
        List<VendorDetail> GetDetails();
        VendorDetail GetVenderbyId(int ProductId);
    }
}