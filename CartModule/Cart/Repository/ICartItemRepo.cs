using Cart.Model;
using System.Collections.Generic;

namespace Cart.Repository
{
    public interface ICartItemRepo
    {
        List<CartItem> GetCartItem(string username);
        bool PostCartItem(string username ,ProductItem pitem, VendorDetail vdetail);
        bool DeleteDetail(int Id);
        CartItem GetDetailbyId(int Id);
    }
}