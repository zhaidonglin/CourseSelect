using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseSelecting.Web.Models.Account
{
    public class StudentRegisterViewModel
    {
        [Required]
        public string LoginId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Pwd { get; set; }

        //[Required]
        //public string StudentNo { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public string Tel { get; set; }
        
        [Required]
        public DateTime EntryDate { get; set; }
    }
}