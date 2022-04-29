using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasktab.Models
{
    public class managelist
    {
        public int id { get; set; }

        public string role { get; set; }

        public string description { get; set; }
        public string modulename { get; set; }

        public int useraccessid { get; set; }

        public bool isadd { get; set; }
        public bool isedit { get; set; }
        public bool isdelete { get; set; }
    }
}