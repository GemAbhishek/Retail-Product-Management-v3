using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Model;
using Product.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProceedToBuy.Provider
{

    public class ProceedToBuyProvider
    {

        IProductRepo con;
        public ProceedToBuyProvider(IProductRepo _con)
        {

            con = _con;
        }

        public  ProceedToBuyProvider(ProceedToBuyProvider @object)
        {

        }

        public IEnumerable<ProductItem> stock;
       

        public virtual ProductItem GetProd(int Id)
        {
            try
            {
                stock = con.Get();
                ProductItem obj = stock.Where(x => x.Id == Id).FirstOrDefault();



                return obj;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}



