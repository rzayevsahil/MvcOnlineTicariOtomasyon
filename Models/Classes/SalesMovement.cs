using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class SalesMovement  
    {
        [Key]
        public int SalesID { get; set; }
        public DateTime Date { get; set; }
        public int Piece { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Situation { get; set; } 
        public int ProductId { get; set; }  
        public int ConcubineId { get; set; }    
        public int EmployeeId { get; set; }  
        public virtual Product Product { get; set; }
        public virtual Concubine Concubine { get; set; }
        public virtual Employee Employee { get; set; }          
    }
}