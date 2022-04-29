using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasktab.Models
{
    public class userright
    {
        public string id { get; set; }

        public string modulename { get; set; }

        //Value of checkbox 
        public int Value { get; set; }
        //description of checkbox 
        public string Text { get; set; }
        //whether the checkbox is selected or not

        public bool IsChecked { get; set; }

        public bool isadd { get; set; }
        public bool isedit { get; set; }
        public bool isdelete { get; set; }


        public string useraccessid { get; set; }
    }
}