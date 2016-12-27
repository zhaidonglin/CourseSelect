using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.TeacherCourse.Dto
{
    [AutoMapFrom(typeof(Education.TeacherCourse))]
    public class TeacherCourseSimpleDto : EntityDto
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 教师编号
        /// </summary>
        public long TeacherId { get; set; }
    }
}
