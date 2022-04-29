using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tasktab.Models
{
    public class userform
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string phone { get; set; }

        public string roleid { get; set; }

        public SelectList roles { get; set; }
    }
   
}