using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tasktab.Models;

namespace tasktab.Controllers
{
    public class taxController : Controller
    {
        //
        // GET: /tax/
        testEntities14 ts = new testEntities14();
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult tabletaxpartial()
        {
            tax t =new tax();
            return View("tabletaxpartial",t);
        }

        public ActionResult leveltablepartial(string level) 
        {
            var db = ts.ruletaxes.Where(x=>x.levels==level).ToList();
            List<tax> li = new List<tax>();
            foreach (var i in db) 
            {
                tax t = new tax();
                t.id = i.id;
                t.taxname = i.taxname;
                t.amount = i.amount;
                t.levels = i.levels;
                li.Add(t);
            }
            taxlist tax = new taxlist();
            tax.taxModule = li;
            return View("leveltablepartial", tax);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            tax t = new tax();
            return View(t);
        }

        [HttpPost]
        public ActionResult Create(tax t)
        {
            ruletax r = new ruletax();
            r.id = t.id;
            r.taxname = t.taxname;
            r.amount = t.amount;
            r.levels = t.levels;
            ts.ruletaxes.AddObject(r);
            ts.SaveChanges();
            string msg = "Save";
            return Json(msg);
        }


        [HttpGet]
        public ActionResult Edit(int id) 
        {
            var t = ts.ruletaxes.Where(x => x.id == id).SingleOrDefault();
            tax ta = new tax();
            ta.id = t.id;
            ta.taxname = t.taxname;
            ta.amount = t.amount;
            ta.levels = t.levels;
            return View("leveleditpartial",ta);
        }


        [HttpPost]
        public ActionResult Edit(tax t)
        {
            var ta = ts.ruletaxes.Where(x => x.id == t.id).SingleOrDefault();
            ta.taxname = t.taxname;
            ta.amount = t.amount;
            ta.levels = t.levels;
            ts.SaveChanges();
            return Json(true);
        }


        public ActionResult Delete(int id)
        {
            var t = ts.ruletaxes.Where(x => x.id == id).SingleOrDefault();
            ts.DeleteObject(t);
            ts.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}
