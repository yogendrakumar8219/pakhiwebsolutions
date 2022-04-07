using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pakhiwebsolutions.Models;
using Microsoft.AspNetCore.Http;

namespace pakhiwebsolutions.ViewModels
{
    public class ContactCreateViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please provide a value for Full Name field"), MaxLength(50, ErrorMessage = "Full Name cannot exceed 50 character")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please provide a value for Phone / Mobile Number field"), MaxLength(30, ErrorMessage = "Mobile / Phone Number cannot exceed 30 characters")]
        [Display(Name = "Phone Number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please provide a value for EmailId field"), MaxLength(50, ErrorMessage = "Email Address cannot exceed 50 character")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-]+$", ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
        [MaxLength(100, ErrorMessage = "Company Name cannot exceed 100 character")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please provide a value for Requirement field"), MaxLength(1000, ErrorMessage = "Requirement cannot exceed 1000 character")]
        [Display(Name = "Requirement Description")]
        public string RequirementDescription { get; set; }
        [Display(Name = "File Upload")]
        public IFormFile DocPath { get; set; }
    }
}
