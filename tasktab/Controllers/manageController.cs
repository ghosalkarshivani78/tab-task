using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasktab.Models;
using System.Text;

namespace tasktab.Controllers
{
    public class manageController : Controller
    {
        //
        // GET: /manage/
        testEntities14 ts = new testEntities14();
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult tablemanagepartial()
        {
            AllAccess access = new AllAccess();

            List<managelist> li = new List<managelist>();
           using (var db = new testEntities14())
           {
               var result = (from a in db.useraccessscrens
                             join b in db.useraccesses on a.useraccessid equals b.id
                             select new
                             {
                                 a.id,
                                 a.useraccessid,
                                 a.modulename,
                                 a.adddata,
                                 a.editdata,
                                 a.deletedata,
                                 b.role,
                                 b.description,
                             }).ToList();
               foreach (var item in result)
               {
                   managelist m = new managelist();
                   m.id = item.id;
                   m.useraccessid = item.useraccessid;
                   m.modulename = item.modulename;
                   m.isadd = (bool)item.adddata;
                   m.isedit = (bool)item.editdata;
                   m.isdelete = (bool)item.deletedata;
                   m.role = item.role;
                   m.description = item.description;
                   li.Add(m);
               }

            
               var userid = Convert.ToInt32(Session["Userid"]);

               var manage = ts.userinfoes.Where(x => x.id == userid).SingleOrDefault();

               var screen = ts.useraccessscrens.Where(x => x.useraccessid == manage.roleid && x.modulename == "User Management").SingleOrDefault();

               access.manaeModule = li;
               if (screen != null)
               {
                   access.modulename = screen.modulename;
                   access.add = (bool)screen.adddata;
                   access.edit = (bool)screen.editdata;
                   access.delete = (bool)screen.deletedata;
               }
               else
               {
                   return View("Errorpartial");
               }
                return View("tablemanagepartial",access);
                //return View("tablemanagepartial", li);
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            
            manage m = new manage();
            return View("newmanagepartial",m );
        }


        [HttpPost]
        public ActionResult Create(manage uf)
        {
            useraccess a1 = new useraccess();
            a1.role = uf.role;
            a1.description = uf.description;
            ts.useraccesses.AddObject(a1);
            ts.SaveChanges();
           

            if (uf.User != null)
            {
                useraccessscren a = new useraccessscren();
                a.modulename = uf.User;
                a.adddata = uf.userisadd;
                a.editdata = uf.userisedit;
                a.deletedata = uf.userisdelete;
                a.useraccessid = a1.id;
                ts.useraccessscrens.AddObject(a);
                ts.SaveChanges();
            }

            if (uf.Contact != null)
            {
                useraccessscren a = new useraccessscren();
                a.modulename = uf.Contact;
                a.adddata = uf.contactisadd;
                a.editdata = uf.contactisedit;
                a.deletedata = uf.contactisdelete;
                a.useraccessid = a1.id;
                ts.useraccessscrens.AddObject(a);
                ts.SaveChanges();
            }

            if (uf.UserManager != null)
            {
                useraccessscren a = new useraccessscren();
                a.modulename = uf.UserManager;
                a.adddata = uf.usermanageisadd;
                a.editdata = uf.usermanageisedit;
                a.deletedata = uf.usermanageisdelete;
                a.useraccessid = a1.id;
                ts.useraccessscrens.AddObject(a);
                ts.SaveChanges();
            }
            return Json(true);
        }


        public ActionResult editcheckboxupdate(int id,bool isedit) 
        {
            var t = ts.useraccessscrens.Where(x => x.id == id).SingleOrDefault();
            t.editdata = isedit;
            ts.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }


        public ActionResult addcheckboxupdate(int id,bool isadd) 
        {
            var t = ts.useraccessscrens.Where(x => x.id == id).SingleOrDefault();
            t.adddata = isadd;
            ts.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult deletecheckboxupdate(int id, bool isdelete)
        {
            var t = ts.useraccessscrens.Where(x => x.id == id).SingleOrDefault();
            t.deletedata = isdelete;
            ts.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }  
}
