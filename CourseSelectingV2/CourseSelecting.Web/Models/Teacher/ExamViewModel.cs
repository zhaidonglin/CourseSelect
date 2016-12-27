using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.TeacherCourse.Dto;

namespace CourseSelecting.Web.Models.Teacher
{
    public class ExamViewModel
    {
        public PagedResultDto<TeacherCourseDto> TeacherCourse { get; set; }
    }
}