using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComPortal.Models
{
    public class Search
    {
        [Display(Name = "Enter Id or Name ")]
        public string str { get; set; }
    }
}
