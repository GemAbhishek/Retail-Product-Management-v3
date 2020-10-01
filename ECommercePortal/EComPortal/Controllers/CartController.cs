using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EComPortal.Models;
using EComPortal.Models.Cart;
using EComPortal.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class CartController : Controller
    {
        readonly log4net.ILog _log4net;

        HttpClient client;
        IConfiguration configuration;
        public CartController(IConfiguration config)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(CartController));

            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }

        public IActionResult Index()
        {
            _log4net.Info(" Index--Get All ");

            string Var = HttpContext.Request.Cookies["UserId"];
            List<CartItem> ls = new List<CartItem>();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/Cart/" + Var).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }
            if (response.IsSuccessStatusCode)
            {

                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<CartItem>>(data);
                return View(ls);
            }
            return View(ls);
        }

        public IActionResult Post(string id)
        {
            _log4net.Info(" Add to cart ");

            ProductItem product = new ProductItem();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseProduct;
            try
            {
                responseProduct = client.GetAsync(client.BaseAddress + "api/product/"+id).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }

            if (responseProduct.IsSuccessStatusCode)
            {
                string dataProduct = responseProduct.Content.ReadAsStringAsync().Result;

                product = JsonConvert.DeserializeObject<ProductItem>(dataProduct);
            }

            if (product.IsAvailable == false)
            {
                return RedirectToAction("Error");
            }

            string Var = HttpContext.Request.Cookies["UserId"];

            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = client.PostAsync(client.BaseAddress + "api/Cart/" + Var, content).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
            
        }

        public IActionResult Delete(int id)
        {
            _log4net.Info(" Delete from cart--id "+id);

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = null;
            try
            {
                response = client.DeleteAsync(client.BaseAddress + "api/Cart/" + id).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
            _log4net.Info(" Error View ");

            return View();
        }

    }
}
