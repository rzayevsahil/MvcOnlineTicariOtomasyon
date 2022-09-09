using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class CargoDetail
    {
        [Key]
        public int CargoDetailID { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(300)]
        public string Explanation { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(10)]
        public string TrackingCode { get; set; } //1234123AB 

        [Column(TypeName = "VarChar")]
        [StringLength(20)]
        public string Employee { get; set; }

        [Column(TypeName = "VarChar")]
        [StringLength(20)]
        public string Receiver { get; set; }  

        public DateTime Date { get; set; }      
    }
}