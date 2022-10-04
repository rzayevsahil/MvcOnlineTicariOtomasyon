using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Class3
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<InvoicePen> InvoicePens { get; set; }
    }
}