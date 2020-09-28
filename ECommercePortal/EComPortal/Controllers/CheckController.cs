using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EComPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EComPortal.Controllers
{
    public class CheckController : Controller
    {
        HttpClient client;
        IConfiguration configuration;
        public CheckController(IConfiguration config)
        {
            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }
        public IActionResult Index()
        {
            string token = HttpContext.Request.Cookies["Token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response;
            try
            {
                response = client.GetAsync(client.BaseAddress + "/check").Result;
            }
            catch
            {
                return Content("Unauthorize");
            }
            if (response.IsSuccessStatusCode)
            {
                string s = response.Content.ReadAsStringAsync().Result;

                return Content("Ok checked with stored token"+"----"+s);
                
            }

            return Content("Unauthorize");

        }
    }
}
