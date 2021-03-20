namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Magazine")]
    public partial class Magazine
    {
        public int MagazineID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        public int? TopicID { get; set; }

        public string MagazineName { get; set; }

        public DateTime? MagazinePostDate { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
