using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        private Context context = new Context();
        public ActionResult Index()
        {
            var values = context.Invoices.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult InvoiceAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoiceAdd(Invoice invoice)
        {
            invoice.Date = DateTime.Now;
            invoice.Hour = DateTime.Now.ToString("HH:mm");
            context.Invoices.Add(invoice);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceGet(int id)
        {
            var invoice = context.Invoices.Find(id);
            return View("InvoiceGet", invoice);
        }

        public ActionResult InvioceUpdate(Invoice i)
        {
            var invoice = context.Invoices.Find(i.InvoiceID);
            invoice.InvoiceSerialNumber = i.InvoiceSerialNumber;
            invoice.InvoiceRowNumber = i.InvoiceRowNumber;
            invoice.Hour = i.Hour;
            invoice.Date = i.Date;
            invoice.Submitter = i.Submitter;
            invoice.Receiver = i.Receiver;
            invoice.TaxAdministration = i.TaxAdministration;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceDetail(int id)
        {
            var values = context.InvoicePens.Where(x => x.InvoiceId == id).ToList();
            ViewBag.invoiceId = id;
            return View(values);
        }
        [HttpGet]
        public ActionResult InvoicePenAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InvoicePenAdd(int id, InvoicePen ip)
        {
            ip.Amount = ip.UnitPrice * ip.Quantity;
            ip.InvoiceId = id;
            context.InvoicePens.Add(ip);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DynamicInvoice()
        {
            Class3 class3 = new Class3();
            class3.Invoices = context.Invoices.ToList();
            class3.InvoicePens = context.InvoicePens.ToList();
            return View(class3);
        }

        public ActionResult InvoiceSave(string InvoiceSerialNumber, string InvoiceRowNumber, DateTime Date, string TaxAdministration, string Hour, string Submitter, string Receiver, string Total, InvoicePen[] invoicePens)
        {
            Invoice invoice = new Invoice();
            invoice.InvoiceSerialNumber = InvoiceSerialNumber;
            invoice.InvoiceRowNumber = InvoiceRowNumber;
            invoice.Date = Date;
            invoice.TaxAdministration = TaxAdministration;
            invoice.Hour = Hour;
            invoice.Submitter = Submitter;
            invoice.Receiver = Receiver;
            invoice.Total = decimal.Parse(Total);
            context.Invoices.Add(invoice);
            foreach (var invoicePen in invoicePens)
            {
                InvoicePen _invoicePen = new InvoicePen();
                _invoicePen.Explanation = invoicePen.Explanation;
                _invoicePen.UnitPrice = invoicePen.UnitPrice;
                _invoicePen.InvoiceId = invoicePen.InvoiceId;
                _invoicePen.Quantity = invoicePen.Quantity;
                _invoicePen.Amount = invoicePen.Amount;
                context.InvoicePens.Add(_invoicePen);
            }
            context.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}