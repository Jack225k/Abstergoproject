using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;

namespace WebApp_OpenIDConnect_DotNet.Models
{
    public class Student
    {
        public int Id { get; set; }
        //
        [Display(Name = "Student Number")]
        [Required(ErrorMessage = "Please enter the student number for this student.")]
        [MaxLength(8)]
        [MinLength(8)]
        public string StudentNumber { get; set; }
        //
        [Display(Name = "Student Name")]
        [Required(ErrorMessage = "Please enter the student name.")]
        public string Name { get; set; }
        //
        [Display(Name = "Student Surname")]
        [Required(ErrorMessage = "Please enter the student surname.")]
        public string Surname { get; set; }
        //
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter the student email address.")]
        public string Email { get; set; }
        //
        [Display(Name = "Telephone Number")]
        public string TelNumber { get; set; }
        //
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter the student cell phone number.")]
        public string MobileNumber { get; set; }
        //
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public string ImageName
        {
            get => StudentNumber;
        }

        public string FilePath { get; set; }
    }
}