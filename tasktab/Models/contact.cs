using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tasktab.Models
{
    public class contact
    {
        public int id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string cityid { get; set; }

        public SelectList cities1 { get; set; }
    
    }
}