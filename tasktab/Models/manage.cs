using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasktab.Models;

namespace tasktab.Models
{
    public class manage
    {
        public int id { get; set; }

        public string role { get; set; }

        public string description { get; set; }

        public int useraccessid { get; set; }

        public bool userisadd { get; set; }
        public bool userisedit { get; set; }
        public bool userisdelete { get; set; }


        public bool contactisadd { get; set; }
        public bool contactisedit { get; set; }
        public bool contactisdelete { get; set; }

        public bool usermanageisadd { get; set; }
        public bool usermanageisedit { get; set; }
        public bool usermanageisdelete { get; set; }


        public string User { get; set; }
        public string Contact { get; set; }
        public string UserManager { get; set; }


        public string modulename { get; set; }

        public List<screenmas> GetModule {get; set;}    
        
        // public useraccess access = new useraccess();
       // public useraccessscren screen =new useraccessscren();
       // public userright right = new userright(); 
    }

    public class mudulemodel
    {
        List<screenmas> list = new List<screenmas>(); 
    }
  
}