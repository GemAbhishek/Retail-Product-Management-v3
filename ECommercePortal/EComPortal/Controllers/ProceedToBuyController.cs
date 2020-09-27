using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EComPortal.Models;
using EComPortal.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class ProceedToBuyController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44388");
        HttpClient client;

        public ProceedToBuyController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Details(string id)
        {
            ProductItem product = new ProductItem();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/ProceedToBuy/" + id).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductItem>(data);
                return View(product);
            }
            return RedirectToAction("Error");

        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
