namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Topic")]
    public partial class Topic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Topic()
        {
            Magazines = new HashSet<Magazine>();
        }

        public int TopicID { get; set; }

        [Column("Topic Name")]
        public string Topic_Name { get; set; }

        [Column("Topic Clousure Date")]
        public DateTime? Topic_Clousure_Date { get; set; }

        [Column("Topic Final Clousure Date")]
        public DateTime? Topic_Final_Clousure_Date { get; set; }

        [Column("Topic Post Date")]
      
        public DateTime? Topic_Post_Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}
