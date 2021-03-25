using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Magazine
    {
        public int MagazineID { get; set; }

        [Display(Name = "Create by")]

        public string Createby { get; set; }
        [Display(Name = "Magazine Name")]

        public string MagazineName { get; set; }


        [Display(Name = "Magazine Post Date")]

        public DateTime? MagazinePostDate { get; set; }

        public int? TopicID { get; set; }
        public virtual ICollection<FileDetail> FileDetails { get; set; }

        public virtual Topic Topic { get; set; }

    }
}