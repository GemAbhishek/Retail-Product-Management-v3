using Product.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Repository
{
    public class ProductRepo : IProductRepo
    {
        public static List<ProductItem> products = new List<ProductItem> {
            new ProductItem{Id=1,Name="Mobile",Description="Android phone",Price=10999,Rating=5,Image_name="Mob.jpg",IsAvailable=true},
            new ProductItem{Id=2,Name="Laptop",Description="Lenovo, 8GB, 1TB",Price=45000,Rating=4,Image_name="Lap.jpg",IsAvailable=true},
            new ProductItem{Id=3,Name="Desktop",Description="HP Brand",Price=25000,Rating=4,Image_name="des.jpg",IsAvailable=false},
            new ProductItem{Id=4,Name="AC",Description="Voltas , 1.5 Top",Price=32000,Rating=5,Image_name="ac.jpg",IsAvailable=true},
            new ProductItem{Id=5,Name="Heater",Description="Prestige, 1KW",Price=1400,Rating=4,Image_name="heater.jpg",IsAvailable=false}
        };
        public List<ProductItem> GetDetails()
        {
            return products;
        }

        public ProductItem GetDetailbyId(int Id)
        {
            return (products.Where(x => x.Id == Id)).FirstOrDefault();
        }
        public ProductItem GetDetailbyName(string name)
        {
            return (products.Where(x => x.Name == name)).FirstOrDefault();
        }

        public bool AddRating(int Id, int data)
        {
            ProductItem Obj = (products.Where(x => x.Id == Id)).FirstOrDefault();
            int val = Obj.Rating;
            Obj.Rating = (val + data) / 2;
            return true;
        }

        public bool AddProduct(ProductItem obj)
        {
            products.Add(obj);
            return true;
        }

        public bool DeleteDetail(int Id)
        {
            ProductItem obj = (products.Where(x => x.Id == Id)).FirstOrDefault();
            products.Remove(obj);
            return true;
        }
    }
}
