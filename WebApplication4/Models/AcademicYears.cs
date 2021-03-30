using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Controllers
{
    public class AcademicYears
    {
        public int AcademicYearsID { get; set; }

        [Required(ErrorMessage = "Please insert Academic Year")]
        [Display(Name = "Academic Year")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "Please insert Academic Year Clousure Date")]
        [Display(Name = "Topic Clousure Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TopicClousureDate { get; set; }

        [Required(ErrorMessage = "Please insert Academic Year Final Clousure Date")]
        [Display(Name = "Topic Final Clousure Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? TopicFinalClousureDate { get; set; }

        
    }
}
