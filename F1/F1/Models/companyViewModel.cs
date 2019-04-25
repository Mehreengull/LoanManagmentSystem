using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F1.Models
{
    public class companyViewModel
    {
        [Required]
        [Display(Name = "Company Code")]
        public string Cid { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string coName { get; set; }

        [Display(Name = "Postal Code")]
        public string postalCode { get; set; }

        [Display(Name = "Contact")]
        public string phoneNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string fax { get; set; }

       

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Link")]
        public string url { get; set; }
    }
}