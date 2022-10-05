using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Concubine
    {
        [Key]
        public int ConcubineID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
        public string ConcubineName { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter yazabilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz!")]
        public string ConcubineSurname { get; set; }
        //public string ConcubineAddress { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string ConcubineCity { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        //[Index("ConcubineMail", IsUnique = true)]
        public string ConcubineMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string ConcubinePassword { get; set; }

        public bool Situation { get; set; }
        public ICollection<SalesMovement> SalesMovements { get; set; }
    }
}