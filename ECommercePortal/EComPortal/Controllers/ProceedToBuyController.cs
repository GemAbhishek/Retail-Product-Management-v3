using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EComPortal.Models;
using EComPortal.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class ProceedToBuyController : Controller
    {
        readonly log4net.ILog _log4net;

        HttpClient client;
        IConfiguration configuration;
        public ProceedToBuyController(IConfiguration config)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));

            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }

        public ActionResult Details(string id)
        {
            _log4net.Info(" proceed to buy--get product ");

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
            _log4net.Info(" Error View ");

            return View();
        }
    }
}
