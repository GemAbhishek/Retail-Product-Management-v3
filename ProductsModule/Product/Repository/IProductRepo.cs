using Product.Model;
using System.Collections.Generic;

namespace Product.Repository
{
    public interface IProductRepo
    {
        ProductItem GetDetailbyId(int Id);
        ProductItem GetDetailbyName(string name);
        List<ProductItem> GetDetails();
        bool AddRating(int Id, int data);
        bool AddProduct(ProductItem obj);
        public bool DeleteDetail(int Id);


    }
}