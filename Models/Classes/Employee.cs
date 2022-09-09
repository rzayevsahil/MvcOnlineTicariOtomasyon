using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "Personel Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeName { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeSurname { get; set; }

        [Display(Name = "Personel Adresi")]
        [Column(TypeName = "Varchar")]
        [StringLength(150)]
        public string EmployeeAddress { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeePhone { get; set; }

        [Display(Name = "Hakkında")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeAbout { get; set; }

        [Display(Name = "Personel Resmi")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string EmployeeImage { get; set; }

        public bool Situation { get; set; }

        public ICollection<SalesMovement> SalesMovements { get; set; }

        public int DepartmentId { get; set; }   
        public virtual Department Department { get; set; }  
    }
}