using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.StudentCourseTime.Dto
{
    [AutoMapFrom(typeof(Education.StudentCourseTime))]
    public class StudentCourseTimeSimpleDto : EntityDto
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public long StudentId { get; set; }

        /// <summary>
        /// 课程时间Id
        /// </summary>
        public int CourseTimeId { get; set; }

        /// <summary>
        /// 随堂分数
        /// </summary>
        public double ClassCredit { get; set; }
    }
}
