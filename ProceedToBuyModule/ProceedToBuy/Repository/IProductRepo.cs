using Product.Model;
using System.Collections;
using System.Collections.Generic;

namespace Product.Repository
{
    public interface IProductRepo
    {
      public IEnumerable <ProductItem> Get();
    }
}