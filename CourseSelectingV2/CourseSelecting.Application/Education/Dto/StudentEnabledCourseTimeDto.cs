using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(CourseTime))]
    public class StudentEnabledCourseTimeDto : EntityDto
    {
        public TeacherCourseDto TeacherCourse { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Address { get; set; }
        public int Times { get; set; }
        public bool EnabledSelecting { get; set; }
        public bool IsSelected { get; set; }
        public bool EnabledEndTime { get; set; }
        public bool EnabledStartTime { get; set; }

    }
}
