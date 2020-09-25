using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Model;
using Product.Repository;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly log4net.ILog _log4net;

        IProductRepo iproduct;
        public ProductController(IProductRepo _db)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(ProductController));

            iproduct = _db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _log4net.Info(" Http GET request--Get All ");

            if (iproduct.GetDetails() == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(iproduct.GetDetails());
        }
        
        /*
         getById || getByName
         */
        [HttpGet("{Var}")]
        public IActionResult GetbyNameOrId(string Var)
        {
            _log4net.Info(" Http GET request by Id/name for = " + Var);

            int id = -1;
            if (!int.TryParse(Var,out id))
            {
                if (iproduct.GetDetailbyName(Var) == null)
                {
                    return BadRequest("Not Found");
                }
                return Ok(iproduct.GetDetailbyName(Var));
            }
            //if var is ID -- int
            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(iproduct.GetDetailbyId(id));

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductItem obj)
        {
            _log4net.Info(" Http PUT request for rating ---- requested id = " + id);

            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest("Not Found");
            }
            if (iproduct.AddRating(id, obj.Rating))
            {
                return Ok("ok");
            }
            return BadRequest("Not Found");
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductItem obj)
        {
            _log4net.Info(" Http POST request ");

            if (iproduct.AddProduct(obj))
                return Ok("Ok");
            return BadRequest("Not Found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _log4net.Info(" Http DELETE request --- requested id = " + id);

            if (iproduct.GetDetailbyId(id) == null)
            {
                return BadRequest("Not Found");
            }
            if (iproduct.DeleteDetail(id))
            {
                return Ok("Ok");
            }
            return BadRequest("Not Found");
        }
        //--------------------------------------------------
        /*
         below get by id is no longer need as GetbyNameOrId is implemented below checking var input
         */
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    if (iproduct.GetDetailbyId(id) == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(iproduct.GetDetailbyId(id));
        //}
        //----------------------------------------------------
    }
}
