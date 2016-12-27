using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.CourseTime.Dto
{
    [AutoMapFrom(typeof(Education.CourseTime))]
    public class CourseTimeSimpleDto : EntityDto
    {
        /// <summary>
        /// 周次
        /// </summary>
        public int Weeks { get; set; }

        /// <summary>
        /// 课程次数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 适合年级
        /// </summary>
        public string FitGrade { get; set; }

        /// <summary>
        /// 上课地点
        /// </summary>
        public string Address { get; set; }
    }
}
