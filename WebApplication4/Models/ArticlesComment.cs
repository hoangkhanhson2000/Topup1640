using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public partial class ArticlesComment
    {
        [Key]
        public int CommentId { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime? CommentOn { get; set; }

        public string CommentBy { get; set; }

        public int? MagazineID { get; set; }

        public virtual Magazine Magazine { get; set; }
    }
}