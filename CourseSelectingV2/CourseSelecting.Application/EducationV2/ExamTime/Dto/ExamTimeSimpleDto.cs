using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.ExamTime.Dto
{
    [AutoMapFrom(typeof(Education.ExamTime))]
    public class ExamTimeSimpleDto : EntityDto
    {
        /// <summary>
        /// 周次
        /// </summary>
        public int Weeks { get; set; }

        /// <summary>
        /// 考试次数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 截止预约时间
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 适合年级
        /// </summary>
        public string FitGrade { get; set; }

        /// <summary>
        /// 考试地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 主考老师
        /// </summary>
        public string Teacher { get; set; }
    }
}
