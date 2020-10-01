using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EComPortal.Models;
using EComPortal.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class ProductController : Controller
    {
        readonly log4net.ILog _log4net;
        HttpClient client;
        IConfiguration configuration;
        public ProductController(IConfiguration config)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(ProductController));
            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }

        public IActionResult Index()
        {
            _log4net.Info(" Index--Get All ");
            List<ProductItem> ls = new List<ProductItem>();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            try
            {
                _log4net.Info(" Http GET request--Get All ");
                response = client.GetAsync(client.BaseAddress + "api/product").Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<ProductItem>>(data);
                return View(ls);
            }
            return RedirectToAction("Error");
        }
        public ActionResult Search()
        {
            _log4net.Info(" Search Action called ");
            return View();
        }
        [HttpPost]
        public ActionResult Search(Search obj)
        {
            return RedirectToAction("Details", new { id = obj.str });
        }
        public ActionResult Details(string id)
        {
            _log4net.Info(" Get detail by id -- "+id);

            ProductItem product = new ProductItem();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/product/" + id).Result;
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
            ViewBag.Error = "Product not found!";
            return View("Search");

        }



        public ActionResult Edit(int id)
        {
            _log4net.Info(" Edit detail by id -- " + id);

            ProductItem product = new ProductItem();

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/product/" + id).Result;
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
        [HttpPost]
        public ActionResult Edit(ProductItem product)
        {
            _log4net.Info(" Post detail Action ");

            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = client.PutAsync(client.BaseAddress + "api/product/"+ product.Id, content).Result;
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
