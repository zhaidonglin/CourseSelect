using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Application.Services.Dto;
using CourseSelecting.Education.Dto;

namespace CourseSelecting.Web.Models.Teacher
{
    public class AddCourseTimeTablesModel
    {
        public ListResultOutput<TeacherCourseDto> TeacherCourses { get; set; }
    }
}