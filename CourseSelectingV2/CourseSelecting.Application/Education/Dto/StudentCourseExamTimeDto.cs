using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(ExamTime))]
    class StudentCourseExamTimeDto
    {
        public TeacherCourseDto TeacherCourse { get; set; }
        public DateTime Start { get; set; }
        public string Address { get; set; }
    }
}
