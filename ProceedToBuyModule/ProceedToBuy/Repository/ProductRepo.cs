using Newtonsoft.Json;
using Product.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Product.Repository
{
    public class ProductRepo : IProductRepo
    {
        /* public static List<ProductItem> products = new List<ProductItem> {
             new ProductItem{Id=1,Name="Mobile",Description="Android phone",Price=10999,Rating=5,Image_name="Mob.jpg",IsAvailable=true},
             new ProductItem{Id=2,Name="Laptop",Description="Lenovo, 8GB, 1TB",Price=45000,Rating=4,Image_name="Lap.jpg",IsAvailable=true},
             new ProductItem{Id=3,Name="Desktop",Description="HP Brand",Price=25000,Rating=4,Image_name="des.jpg",IsAvailable=false},
             new ProductItem{Id=4,Name="AC",Description="Voltas , 1.5 Top",Price=32000,Rating=5,Image_name="ac.jpg",IsAvailable=false},
             new ProductItem{Id=5,Name="Heater",Description="Prestige, 1KW",Price=1400,Rating=4,Image_name="heater.jpg",IsAvailable=false}
         };*/
        Uri baseaddress = new Uri("https://localhost:44321/api/Product");
        HttpClient client;
        public ProductRepo()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;

        }
        

        public IEnumerable <ProductItem >Get()
        {
            try
            {
                List<ProductItem> ls = new List<ProductItem>();
                
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    ls = JsonConvert.DeserializeObject<List<ProductItem>>(data);
                   
                    return ls;
                }
               
                return ls;
            }
            catch (Exception e)
            {
               
                throw e;
            }

        }


        /*
        public int AddDetail(Book data)
        {
            //return 1 if done
            if (db != null)
            {
                db.Books.Add(data);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int DeleteDetail(int Id)
        {
            if (db != null)
            {
                Book book = db.Books.FirstOrDefault(x => x.Id == Id);

                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public Book GetDetail(int? Id)
        {
            if (db != null)
            {
                return (db.Books.Where(x => x.Id == Id)).FirstOrDefault();
            }
            return null;
        }

        public List<Book> GetDetails()
        {
            if (db != null)
            {
                return db.Books.ToList();
            }
            return null;
        }

        public int UpdateDetail(int id, Book data)
        {
            if (db != null)
            {
                try
                {
                    data.Id = id;
                    db.Books.Update(data);
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        */
    }
}
