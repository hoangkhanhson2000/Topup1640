using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public partial class Topic
    {
        public int TopicID { get; set; }

        [Required(ErrorMessage = "Please insert Topic Name")]
        [Display(Name = "Topic Name")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "Please insert Topic Clousure Date")]
        [Display(Name = "Topic Clousure Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TopicClousureDate { get; set; }

        [Required(ErrorMessage = "Please insert Topic Final Clousure Date")]
        [Display(Name = "Topic Final Clousure Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TopicFinalClousureDate { get; set; }

        [Display(Name = "Topic Post Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TopicPostDate { get; set; }
        public virtual ICollection<Magazine> Magazines { get; set; }
    }
}