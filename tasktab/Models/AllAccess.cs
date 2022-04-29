using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasktab.Models
{
    public class AllAccess
    {
     

        public string modulename { get; set; }

        public bool add { get; set; }

        public bool edit { get; set; }

        public bool delete { get; set; }

        public List<userform> userModule = new List<userform>();

        public List<contact> contactModule = new List<contact>();

        public List<managelist> manaeModule = new List<managelist>();
    }
}