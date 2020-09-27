using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EComPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EComPortal.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44388/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Auth(User user)
        {
            string content = JsonConvert.SerializeObject(user);
            StringContent data = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync(client.BaseAddress + "/login", data).Result;
            }
            catch
            {
                //Session["log"] = Login;

                return RedirectToAction("Error");
            }
            string token = response.Content.ReadAsStringAsync().Result;

            if (token!="error")
            {
                HttpContext.Response.Cookies.Append("Token", token);
                HttpContext.Response.Cookies.Append("UserId", user.Username);
                //Session["log"] = Logout;
                return RedirectToAction("SeeProduct");

            }

            //Session["log"] = Login;

            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("UserId");
            ViewBag.Error = "Invalid UserName or password";
            return View("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("UserId");
            return RedirectToAction("Login");
        }

        public IActionResult SeeProduct()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
