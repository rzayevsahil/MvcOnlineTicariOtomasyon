using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Category
    {
        [Key]//üzerinde bulunduğum değişkeni primary key yapıyorum
        public int CategoryID { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CategoryName { get; set; }

        //ICollection birden fazla ürünü bir arada tutar, yani her bir kategoride 1'den fazla ürün yer alabilir
        public ICollection<Product> Products { get; set; } 
    }
}