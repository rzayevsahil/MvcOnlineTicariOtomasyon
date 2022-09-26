using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    public class Message
    {
        [Key] 
        public int MessageID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageSender { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageRecipient { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageSubject { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string MessageContents { get; set; }

        [Column(TypeName = "Smalldatetime")]
        public DateTime MessageDate { get; set; }
    }
}