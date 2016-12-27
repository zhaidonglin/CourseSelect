using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(Education.StudentCourseTime))]
    public class SSCourseTimeDto : EntityDto
    {
        public CourseTimeDto CourseTime { get; set; }
    }
}
