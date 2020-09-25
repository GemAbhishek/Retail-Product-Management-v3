using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProceedToBuy.Provider;
using Product.Model;

namespace ProceedToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {




       public readonly ProceedToBuyProvider obj;
        readonly log4net.ILog _log4net;




        public ProceedToBuyController(ProceedToBuyProvider _obj)
        {
            obj = _obj;
            _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));


            



        }

        /*
         again getting the product detail to assure correct data of product ...client may temper the data......
        */
        [HttpGet("{id}")]
        public IActionResult GetbyNameOrId(int id)
        {
            _log4net.Info("ProceedTo Buy Controller getbyid action method is called");

            ProductItem pr = obj.GetProd(id);

           
            if (pr!= null)
            {
                return Ok(pr);

            }

            return BadRequest("Record not found");
        }

    }
}
