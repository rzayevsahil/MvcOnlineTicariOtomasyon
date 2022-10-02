using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class ConcubineController : Controller
    {
        // GET: Concubine
        private Context context = new Context();
        public ActionResult Index()
        {
            var concubines = context.Concubines.Where(c => c.Situation == true).ToList();
            return View(concubines);
        }

        [HttpGet]
        public ActionResult ConcubineAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConcubineAdd(Concubine concubine)
        {
            concubine.Situation = true;
            context.Concubines.Add(concubine);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConcubineDelete(int id)
        {
            var concubine = context.Concubines.Find(id);
            concubine.Situation = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConcubineGet(int id)
        {
            var concubine = context.Concubines.Find(id);
            return View("ConcubineGet", concubine);
        }

        public ActionResult ConcubineUpdate(Concubine c)
        {
            if (!ModelState.IsValid)
            {
                return View("ConcubineGet");
            }
            var concubine = context.Concubines.Find(c.ConcubineID);
            concubine.ConcubineName = c.ConcubineName;
            concubine.ConcubineSurname = c.ConcubineSurname;
            concubine.ConcubineCity = c.ConcubineCity;
            concubine.ConcubineMail = c.ConcubineMail;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerSales(int id)
        {
            var values = context.SalesMovements.Where(s => s.ConcubineId == id).ToList();
            var employeeNameSurname = context.Concubines.Where(c => c.ConcubineID == id).Select(y => y.ConcubineName + " " + y.ConcubineSurname).FirstOrDefault();
            ViewBag.employeeNameSurname = employeeNameSurname;
            return View(values);
        }
    }
}