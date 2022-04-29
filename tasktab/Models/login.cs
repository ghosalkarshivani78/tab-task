using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tasktab.Models
{
    public class login3
    {
        [Required]
        public string username { get; set; }

         [Required]
        public string password { get; set; }
    }
}