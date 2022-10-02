using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        // GET: Sales
        private Context context = new Context();
        public ActionResult Index()
        {
            var values = context.SalesMovements.Where(x => x.Employee.Situation == true).Where(y => y.Situation == true).ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult SalesAdd()
        {
            List<SelectListItem> value1 = (from x in context.Products.Where(p => p.Situation == true).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();
            List<SelectListItem> value2 = (from x in context.Concubines.Where(c => c.Situation == true).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = x.ConcubineName + " " + x.ConcubineSurname,
                                               Value = x.ConcubineID.ToString()
                                           }).ToList();
            List<SelectListItem> value3 = (from x in context.Employees.Where(e => e.Situation == true).ToList()
                                           select new SelectListItem()
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.value3 = value3;
            return View();
        }

        [HttpPost]
        public ActionResult SalesAdd(SalesMovement sm)
        {
            sm.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            sm.Situation = true;
            context.SalesMovements.Add(sm);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SalesGet(int id)
        {
            List<SelectListItem> value1 = (from x in context.Products.Where(p => p.Situation == true).ToList()
                select new SelectListItem()
                {
                    Text = x.ProductName,
                    Value = x.ProductID.ToString()
                }).ToList();
            List<SelectListItem> value2 = (from x in context.Concubines.Where(c => c.Situation == true).ToList()
                select new SelectListItem()
                {
                    Text = x.ConcubineName + " " + x.ConcubineSurname,
                    Value = x.ConcubineID.ToString()
                }).ToList();
            List<SelectListItem> value3 = (from x in context.Employees.Where(e => e.Situation == true).ToList()
                select new SelectListItem()
                {
                    Text = x.EmployeeName + " " + x.EmployeeSurname,
                    Value = x.EmployeeID.ToString()
                }).ToList();
            ViewBag.value1 = value1;
            ViewBag.value2 = value2;
            ViewBag.value3 = value3;
            var value = context.SalesMovements.Find(id);
            return View("SalesGet", value);
        }

        public ActionResult SalesUpdate(SalesMovement salesMovement)
        {
            var value = context.SalesMovements.Find(salesMovement.SalesID);
            value.ConcubineId = salesMovement.ConcubineId;
            value.Piece = salesMovement.Piece;
            value.Price = salesMovement.Price;
            value.EmployeeId = salesMovement.EmployeeId;
            value.Date = salesMovement.Date;
            value.TotalAmount = salesMovement.TotalAmount;
            value.ProductId = salesMovement.ProductId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SalesDetail(int id)
        {
            var values = context.SalesMovements.Where(s => s.SalesID == id).ToList();
            return View(values);
        }
    }
}