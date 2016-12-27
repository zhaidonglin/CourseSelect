using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseSelecting.Web.Models.Account
{
    public class TeacherRegisterViewModel
    {
        [Required]
        public string LoginId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Pwd { get; set; }

        [Required]
        public string TeacherNo { get; set; }

        [Required]
        public string Department { get; set; }
    }
}