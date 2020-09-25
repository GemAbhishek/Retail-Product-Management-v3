using Cart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Repository
{
    public class CartItemRepo : ICartItemRepo
    {
        public static List<CartItem> cartlist = new List<CartItem>
        {
            new CartItem{Id=1,UserName ="Test1" , ProductName ="Mobile", Price = 10999,
                Description ="Lenovo", VendorName ="aaaaaa" , DeliveryDate =DateTime.Parse("12-10-2020"), DeliveryCharge=80 }

        };

        public CartItem GetDetailbyId(int Id)
        {
            return (cartlist.Where(x => x.Id == Id)).FirstOrDefault();
        }

        public List<CartItem> GetCartItem(string username)
        {
            List<CartItem> ls = new List<CartItem>();
            foreach (CartItem c in cartlist)
            {
                if (c.UserName == username)
                {
                    ls.Add(c);
                }
            }
            return ls;
        }
        public bool PostCartItem(string username, ProductItem pitem, VendorDetail vdetail)
        {
            CartItem cItem = new CartItem();
            cItem.UserName = username;
            cItem.ProductName = pitem.Name;
            cItem.Price = pitem.Price;
            cItem.Description = pitem.Description;
            cItem.VendorName = vdetail.Name;
            cItem.DeliveryCharge = vdetail.DeliveryCharge;
            //date has been taken by default as vendor does not return date as discribed in document......
            cItem.DeliveryDate = DateTime.Parse("12-10-2020");

            if (pitem.IsAvailable==false)
            {
                cartlist.Add(cItem);
                return true;
            }
            return false;
        }

        public bool DeleteDetail(int Id)
        {
            CartItem obj = (cartlist.Where(x => x.Id == Id)).FirstOrDefault();
            cartlist.Remove(obj);
            return true;
        }

    }
}
