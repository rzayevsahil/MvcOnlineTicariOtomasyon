using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
using Newtonsoft.Json;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ToDoController : Controller
    {
        // GET: ToDo
        Context context = new Context();
        public ActionResult Index()
        {
            var value1 = context.Concubines.Count().ToString();
            ViewBag.val1 = value1;
            var value2 = context.Products.Count().ToString();
            ViewBag.val2 = value2;
            var value3 = context.Categories.Count().ToString();
            ViewBag.val3 = value3;
            var value4 = (from x in context.Concubines select x.ConcubineCity).Distinct().Count().ToString();
            ViewBag.val4 = value4;

            var todos = context.ToDoes.ToList();
            return View(todos);
        }

        [HttpGet]
        public ActionResult ToDoAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToDoAdd(ToDo toDo)
        {   
            toDo.Situation = false;
            context.ToDoes.Add(toDo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ToDoGet(int id)
        {
            var todo = context.ToDoes.Find(id);
            ViewBag.id = todo.Title;
            return View(todo);
        }
        [HttpPost]
        public ActionResult ToDoGet(ToDo td)
        {
            var todo = context.ToDoes.Find(td.ToDoID);
            todo.ToDoID = td.ToDoID;
            todo.Title = td.Title;
            todo.Situation = td.Situation;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}