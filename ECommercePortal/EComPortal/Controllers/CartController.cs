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
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class CartController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44388");
        HttpClient client;

        public CartController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            string Var = TokenInfo.UserName;
            List<CartItem> ls = new List<CartItem>();

            string token = TokenInfo.StringToken;
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
            ProductItem product = new ProductItem();

            string token = TokenInfo.StringToken;
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

            string Var = TokenInfo.UserName;

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
            string token = TokenInfo.StringToken;
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
            return View();
        }

    }
}
