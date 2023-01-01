using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        Context context = new Context();
        public ActionResult Index(string p, int page = 1)
        {
            //context.Departments.Where(d => d.Situation == true).ToList();
            var values = from d in context.Departments where d.Situation == true select d;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.DepartmentName.Contains(p));
            }

            var departments = values.ToList().ToPagedList(page, 5);
            return View(departments);
        }
        //[Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult DepartmentAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmentAdd(Department department)
        {
            department.Situation = true;
            context.Departments.Add(department);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmentDelete(int id)
        {
            var department = context.Departments.Find(id);
            department.Situation = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmentGet(int id)
        {
            var department = context.Departments.Find(id);
            return View("DepartmentGet", department);
        }
        [HttpPost]
        public ActionResult DepartmentUpdate(Department d)
        {
            var department = context.Departments.Find(d.DepartmentID);
            department.DepartmentName = d.DepartmentName;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmentDetail(int id)
        {
            var values = context.Employees.Where(x => x.DepartmentId == id).Where(y => y.Situation == true).ToList();
            var departmentName = context.Departments.Where(x => x.DepartmentID == id).Select(y => y.DepartmentName)
                .FirstOrDefault();
            //var departmentName = context.Departments.Find(id).DepartmentName;
            ViewBag.departmentName = departmentName;
            return View(values);
        }

        public ActionResult DepartmentEmployeeSales(int id)
        {
            var values = context.SalesMovements.Where(x => x.EmployeeId == id).Where(y => y.Situation == true).ToList();
            var departmentId = context.Employees.Where(x => x.EmployeeID == id).Select(y => y.DepartmentId).FirstOrDefault();
            var employeeNameSurname = context.Employees.Where(e => e.EmployeeID ==
                                                                   id).Select(y =>
                y.EmployeeName + " " + y.EmployeeSurname).FirstOrDefault();
            ViewBag.departmentId = departmentId;
            ViewBag.employeeNameSurname = employeeNameSurname;
            return View(values);
        }
    }
}