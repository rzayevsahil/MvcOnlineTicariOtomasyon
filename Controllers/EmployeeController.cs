using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private Context context = new Context();
        public ActionResult Index(string p, int page = 1)
        {
            var values = from e in context.Employees where e.Situation == true select e;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.EmployeeName.Contains(p));
            }
            var employees = values.ToList().ToPagedList(page, 5);
            return View(employees);
        }

        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            List<SelectListItem> value = (from department in context.Departments.ToList()
                                          select new SelectListItem
                                          {
                                              Text = department.DepartmentName,
                                              Value = department.DepartmentID.ToString()
                                          }).ToList();
            ViewBag.val = value;
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeAdd(Employee employee)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                employee.EmployeeImage = "/Images/" + fileName + extension;
            }
            employee.Situation = true;
            var e = context.Employees.Add(employee);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmployeeGet(int id)
        {
            List<SelectListItem> value = (from department in context.Departments.ToList()
                                          select new SelectListItem
                                          {
                                              Text = department.DepartmentName,
                                              Value = department.DepartmentID.ToString()
                                          }).ToList();
            ViewBag.val = value;
            var employee = context.Employees.Find(id);
            return View("EmployeeGet", employee);
        }

        public ActionResult EmployeeUpdate(Employee e)
        {
            var employee = context.Employees.Find(e.EmployeeID);
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/" + fileName + extension;
                Request.Files[0].SaveAs(Server.MapPath(path));
                employee.EmployeeImage = "/Images/" + fileName + extension;
            }
            employee.EmployeeName = e.EmployeeName;
            employee.EmployeeSurname = e.EmployeeSurname;
            //employee.EmployeeImage = e.EmployeeImage;
            employee.DepartmentId = e.DepartmentId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmployeeDelete(int id)
        {
            var employee = context.Employees.Find(id);
            employee.Situation = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EmployeeDetail()
        {
            var query = context.Employees.Where(x => x.Situation == true).ToList();
            return View(query);
        }
    }
}