using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasktab.Models;

namespace tasktab.Controllers
{
    public class contactController : Controller
    {
        //
        // GET: /contact/
        testEntities14 ts = new testEntities14();

        public ActionResult mainpage() 
        {
            List<contact> li = new List<contact>();
            return View(li);
        }

        public ActionResult tablecontactpartial()
        {
            AllAccess access = new AllAccess();
            List<contact> li = new List<contact>();
            var db=ts.usercities.ToList();
            foreach (var i in db) 
            {
                contact uc = new contact();
                uc.id = i.id;
                uc.name = i.name;
                uc.address = i.address;
                uc.phone = i.phone;
                uc.cityid = ts.cities.Where(x => x.cityid == i.cityid).SingleOrDefault().cityname;
                li.Add(uc);
            }

            int userid = Convert.ToInt32(Session["Userid"]);

            var contact = ts.userinfoes.Where(x => x.id == userid).SingleOrDefault();

            var screen = ts.useraccessscrens.Where(x => x.useraccessid == contact.roleid && x.modulename == "Contact").SingleOrDefault();

            access.contactModule = li;

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
            return View("tablecontactpartial",access);
        }

      

        [HttpGet]
        public ActionResult Create() 
        {
            int id = Convert.ToInt32(Session["Userid"]);
            contact c = new contact();
            var citylist = ts.cities.ToList();
            c.cities1 = new SelectList(citylist, "cityid", "cityname");
            return View("newcontactpartial",c);
        }


        [HttpPost]
        public ActionResult Create(contact u)
        {
            contact c =new contact();
            var citylist = ts.cities.ToList();
            c.cities1 = new SelectList(citylist, "cityid", "cityname");
            try
            {
                usercity ui = new usercity();
                ui.id = u.id;
                ui.name = u.name;
                ui.address = u.address;
                ui.phone = u.phone;
                ui.cityid =Convert.ToInt32(u.cityid);
                ts.usercities.AddObject(ui);
                ts.SaveChanges();
                bool msg = false;
                return Json(msg);
                //return RedirectToAction("mainpage","Home");
            }
            catch
            {
                bool msg = true;
                return Json(msg);
            }
            
        }

        [HttpGet]
        public ActionResult Edit(int id) 
        {
            
          
            var t=ts.usercities.Where(x=>x.id==id).SingleOrDefault();
            contact u = new contact();
            var citylist = ts.cities.ToList();
            u.cities1 = new SelectList(citylist, "cityid", "cityname");
            u.id = t.id;
            u.name = t.name;
            u.address = t.address;
            u.phone = t.phone;
            u.cityid = t.cityid.ToString();
            return View("editcontactpartial",u);
        }

        [HttpPost]
        public ActionResult Edit(contact c) 
        {
            contact ci = new contact();
            var citylist = ts.cities.ToList();
            ci.cities1 = new SelectList(citylist, "cityid", "cityname");
            try
            {
                var t = ts.usercities.Where(x => x.id == c.id).SingleOrDefault();
                t.id = c.id;
                t.name = c.name;
                t.address = c.address;
                t.phone = c.phone;
                t.cityid =Convert.ToInt32(c.cityid);
                ts.SaveChanges();
                bool msg = false;
                return Json(msg);
                //return RedirectToAction("mainpage","Home");
            }
            catch 
            {
                bool msg = true;
                return Json(msg);
                //return View(c);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id) 
        {
            try
            {
                var t = ts.usercities.Where(x => x.id == id).SingleOrDefault();
                ts.DeleteObject(t);
                ts.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("mainpage","Home");
        }


        
    }
}
