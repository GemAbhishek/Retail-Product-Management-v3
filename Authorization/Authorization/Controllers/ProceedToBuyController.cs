using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProceedToBuyController : ControllerBase
    {
        Uri baseAddress = new Uri("https://localhost:44321");
        HttpClient client;

        public ProceedToBuyController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        

        [HttpGet("{Var}")]
        public IActionResult GetbyNameOrId(string Var)
        {
            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(client.BaseAddress + "api/Product/" + Var).Result;
            }
            catch
            {
                return BadRequest("Micro Not working");
            }
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return Ok(data);
            }
            return BadRequest();
        }
    }
}
