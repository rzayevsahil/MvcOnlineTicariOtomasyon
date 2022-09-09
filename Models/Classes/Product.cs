using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string ProductName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Brand { get; set; }
        public short Stock { get; set; }
        public decimal PurchhasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public bool Situation { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string ProductImage { get; set; }

        //frontend tarafında sıkıntı çıkarmasın diye id ilişkisi özellikle bunu ekliyoruz
        public int CategoryId { get; set; }

        //Bir ürün sadece bir category'e aittir
        public virtual Category Category { get; set; }
        public ICollection<SalesMovement> SalesMovements { get; set; }
    }
}