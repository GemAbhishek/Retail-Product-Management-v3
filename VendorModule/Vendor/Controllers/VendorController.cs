using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendor.Repository;

namespace Vendor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        IVendorDetailRepo ivendor;
        public VendorController(IVendorDetailRepo _db)
        {
            ivendor = _db;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if ((ivendor.GetVenderbyId(id)) == null)
            {
                return BadRequest("No vendor");
            }
            return Ok(ivendor.GetVenderbyId(id));
        }
    }
}
