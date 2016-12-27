using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.CourseTime.Dto;
using CourseSelecting.Application.Users.Student.Dto;

namespace CourseSelecting.Application.EducationV2.StudentCourseTime.Dto
{
    [AutoMapFrom(typeof(Education.StudentCourseTime))]
    public class StudentCourseTimeDto : EntityDto
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public StudentDto Student { get; set; }

        /// <summary>
        /// 所选的课程时间信息
        /// </summary>
        public CourseTimeDto CourseTime { get; set; }

        /// <summary>
        /// 随堂分数
        /// </summary>
        public double ClassCredit { get; set; }
    }
}
