using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasktab.Models;

namespace tasktab.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        testEntities14 ts = new testEntities14();

        public ActionResult Index()
        {
            login3 ui = new login3();
            return View(ui);
        }

        [HttpPost]
        public ActionResult login(login3 ui) 
        {
            var db = ts.userinfoes.Where(x => x.email == ui.username && x.password == ui.password).SingleOrDefault();
            if (db !=null)
            {
                Session["Userid"] = db.id;
                return RedirectToAction("mainpage");
            }
            else
            {
                ViewBag.msg = "Username or Password Incorrect";
                return View("Index",ui);
            }
        }

        public ActionResult mainpage() 
        {
            List<userform> li = new List<userform>();
            return View(li);
        }

        public ActionResult tablepartial()
        {
            AllAccess access = new AllAccess();

            var db = ts.userinfoes.ToList();
            List<userform> li = new List<userform>();
            foreach (var i in db)
            {
                userform u = new userform();
                u.id = i.id;
                u.name = i.name;
                u.email = i.email;
                u.phone = i.phone;
                u.roleid = ts.useraccesses.Where(x => x.id == i.roleid).SingleOrDefault() == null ? null : ts.useraccesses.Where(x => x.id == i.roleid).SingleOrDefault().role;
                li.Add(u);
            }
            int userid = Convert.ToInt32(Session["Userid"]);
            var user = ts.userinfoes.Where(x => x.id == userid).SingleOrDefault();
            var screen = ts.useraccessscrens.Where(x => x.useraccessid == user.roleid && x.modulename == "User").SingleOrDefault();

            access.userModule = li;
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
      
            return View("tablepartial", access);
        }

        //public ActionResult CreateNew() 
        //{
        //    userform u = new userform();
        //    return View("CreateNewpartial",u);
        //}

        public ActionResult Create()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        {
            int id = Convert.ToInt32(Session["Userid"]);
            userform u = new userform();
            var rolelist = ts.useraccesses.ToList();
            u.roles = new SelectList(rolelist, "id", "role");
            return View("CreateNewpartial", u);
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(userform uf)
        {
            userform ui = new userform();
            var rolelist = ts.useraccesses.ToList();
            ui.roles = new SelectList(rolelist, "id", "role");
            try
            {
                var t = ts.userinfoes.Where(x => x.email == uf.email).SingleOrDefault();
                if (t == null)
                {
                    userinfo u = new userinfo();
                    u.name = uf.name;
                    u.email = uf.email;
                    u.password = uf.password;
                    u.phone = uf.phone;
                    u.roleid =Convert.ToInt32(uf.roleid);
                    ts.userinfoes.AddObject(u);
                    ts.SaveChanges();
                    string msg = "Save";
                    return Json(msg);
                    //return RedirectToAction("mainpage");
                }
                else 
                {
                    string msg = "Duplicate Email";
                    return Json(msg);
                }            
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
            var uf = ts.userinfoes.Where(x => x.id == id).SingleOrDefault();
            userform u = new userform();
            var rolelist = ts.useraccesses.ToList();
            u.roles = new SelectList(rolelist, "id", "role");
            u.id = uf.id;
            u.name = uf.name;
            u.email = uf.email;
            u.password = uf.password;
            u.phone = uf.phone;
            u.roleid = uf.roleid.ToString();
            return View("editpartial",u);
        }

        [HttpPost]
        public ActionResult Edit(userform uf)
        {
            userform u = new userform();
            var rolelist = ts.useraccesses.ToList();
            u.roles = new SelectList(rolelist, "id", "role");
            try
            {
                var ta = ts.userinfoes.Where(x => x.id == uf.id).SingleOrDefault();
                ta.name = uf.name;
                ta.email = uf.email;
                ta.password = uf.password;
                ta.phone = uf.phone;
                ta.roleid =Convert.ToInt32(uf.roleid);
                ts.SaveChanges();
                bool msg = false;
                return Json(msg);
                //return RedirectToAction("mainpage");
            }
            catch 
            {
                bool msg = true;
                return Json(msg);
                //return View(uf);
            }
        }

        public ActionResult Delete(int id) 
        {
            var t = ts.userinfoes.Where(x => x.id == id).SingleOrDefault();
            ts.DeleteObject(t);
            ts.SaveChanges();
            return RedirectToAction("mainpage");
        }
    }
}
