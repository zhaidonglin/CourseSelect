using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.Semester.Dto
{
    [AutoMapFrom(typeof(Education.Semester))]
    public class SemesterDto : EntityDto
    {
        /// <summary>
        /// 学期名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学期开始时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 学期结束时间
        /// </summary>
        public DateTime End { get; set; }
    }
}
