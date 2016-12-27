using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(ExamTime))]
    public class StudentEnabledExamTimeDto : EntityDto
    {
        public TeacherCourseDto TeacherCourse { get; set; }
        public DateTime Start { get; set; }

        private DateTime end;
        public DateTime End
        {
            get
            {
                if (end == new DateTime(1900, 1, 1)) end = Start;
                return end;
            }
            set { end = value; }
        }
        public string Address { get; set; }
        public int Times { get; set; }
        public bool EnabledSelecting { get; set; }
        public bool EnabledEndTime { get; set; }
        public bool EnabledStartTime { get; set; }
        public bool IsSelected { get; set; }
    }
}
