using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Users.Dto;   //10

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(CourseTime))]
    public class CourseTimeDto : EntityDto
    {
        public TeacherCourseDto TeacherCourse { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Address { get; set; }
        public int Times { get; set; }
        public string FitGrade { get; set; } = ",";
        public int TeacherId { get; set; }            //10
        public StudentListDto Student { get; set; }    //10
    }
}
