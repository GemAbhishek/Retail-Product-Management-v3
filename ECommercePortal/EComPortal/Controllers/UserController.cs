using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EComPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class UserController : Controller
    {
        readonly log4net.ILog _log4net;

        HttpClient client;
        IConfiguration configuration;
        public UserController(IConfiguration config)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(UserController));

            configuration = config;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration["Authorize"]);
        }
        public IActionResult Login()
        {
            _log4net.Info(" login view ");

            return View();
        }
        public IActionResult Auth(User user)
        {
            _log4net.Info(" Auth Action ");

            string content = JsonConvert.SerializeObject(user);
            StringContent data = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync(client.BaseAddress + "api/login", data).Result;
            }
            catch
            {              
                return RedirectToAction("Error");
            }
            string token = response.Content.ReadAsStringAsync().Result;

            if (token!="error")
            {
                _log4net.Info("Login succcess");

                HttpContext.Response.Cookies.Append("Token", token);
                HttpContext.Response.Cookies.Append("UserId", user.Username);
                return RedirectToAction("SeeProduct");

            }

            _log4net.Info(" Invalid UserName or password ");

            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("UserId");
            ViewBag.Error = "Invalid UserName or password";
            return View("Login");
        }
        public IActionResult Logout()
        {
            _log4net.Info("logout");

            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("UserId");
            return RedirectToAction("Login");
        }

        public IActionResult SeeProduct()
        {
            _log4net.Info("Login succcess View");

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
