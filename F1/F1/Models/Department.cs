using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F1.Models
{
    public class Department
    {
        [Required]
        [Display(Name = "Department ID")]
        public int DID { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string DName { get; set; }

        [Display(Name = "Number of Employess")]
        public string DEmployee { get; set; }


    }
}