using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EComPortal.Models;
using EComPortal.Models.Product;
using EComPortal.Models.Wishlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class WishlistController : Controller
    {
        readonly log4net.ILog _log4net;

        private readonly ApplicationDbContext _db;

        HttpClient client;
        IConfiguration configuration;
        public WishlistController(IConfiguration config, ApplicationDbContext db)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(WishlistController));

            _db = db;
            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }
       
        public IActionResult Index()
        {
            _log4net.Info(" Index--Get All ");

            string user = HttpContext.Request.Cookies["UserId"];
            List<WishlistItem> items = new List<WishlistItem>();
            items = _db.WishlistItems.ToList();

            List<WishlistItem> listUser = new List<WishlistItem>();
            foreach (WishlistItem w in items)
            {
                if (w.UserName == user)
                {
                    listUser.Add(w);
                }
            }           
            return View(listUser);
        }

        public IActionResult Delete(int id)
        {
            _log4net.Info(" Delete--id-- " + id);

            WishlistItem item = new WishlistItem();
            item =_db.WishlistItems.FirstOrDefault(x => x.Id == id);
            _db.WishlistItems.Remove(item);
            _db.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult AddItem(string id)
        {
            _log4net.Info(" Add item to Wishlist ");

            ProductItem product = new ProductItem();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            
            response = client.GetAsync(client.BaseAddress + "api/product/" + id).Result;

            string data = response.Content.ReadAsStringAsync().Result;
            product = JsonConvert.DeserializeObject<ProductItem>(data);

            WishlistItem item = new WishlistItem();
            item.UserName = HttpContext.Request.Cookies["UserId"];
            item.ProductName = product.Name;
            item.ProductId = product.Id;
            item.Price = product.Price;
            item.Description = product.Description;

            _db.WishlistItems.Add(item);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        
    }
}
