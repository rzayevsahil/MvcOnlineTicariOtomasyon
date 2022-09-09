using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }


        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string InvoiceSerialNumber { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string InvoiceRowNumber { get; set; }
        public DateTime Date { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string TaxAdministration { get; set; }

        [Column(TypeName = "char")]
        [StringLength(5)]
        public string Hour { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Submitter { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Receiver { get; set; }

        public decimal Total { get; set; }      

        public ICollection<InvoicePen> InvoicePens { get; set; }    
    }
}