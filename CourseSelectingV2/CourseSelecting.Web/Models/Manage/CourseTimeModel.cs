using CourseSelecting.Education.Dto;
using CourseSelecting.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseSelecting.Web.Models.Manage
{
    public class CourseTimeModel
    {
        public CourseTimeDto CourseTime { get; set; }
        public StudentDetailsDto Student { get; set; }
    }
}