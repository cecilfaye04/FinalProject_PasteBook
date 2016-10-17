using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PasteBook
{
    public class UserModel
    {
        public int ID { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string User_Name { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Salt { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string Last_Name { get; set; }

        [Display(Name = "Birthdate")]
        [Required]
        public System.DateTime Birthday { get; set; }

        [Display(Name = "Country")]
        public Nullable<int> Country_ID { get; set; }

        [Display(Name = "Mobile No")]
        public string Mobile_No { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public byte[] Profile_Pic { get; set; }
        public System.DateTime Date_Created { get; set; }
        public string About_Me { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        public string Email_Address { get; set; }
    }
}