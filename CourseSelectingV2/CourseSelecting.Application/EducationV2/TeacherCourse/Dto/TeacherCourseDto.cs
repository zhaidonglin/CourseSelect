using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.Course.Dto;
using CourseSelecting.Application.EducationV2.CourseTime.Dto;
using CourseSelecting.Application.EducationV2.ExamTime.Dto;
using CourseSelecting.Application.Users.Teacher.Dto;

namespace CourseSelecting.Application.EducationV2.TeacherCourse.Dto
{
    [AutoMapFrom(typeof(Education.TeacherCourse))]
    public class TeacherCourseDto : EntityDto
    {
        /// <summary>
        /// 课程
        /// </summary>
        public CourseDto Course { get; set; }

        /// <summary>
        /// 教师
        /// </summary>
        public TeacherDto Teacher { get; set; }
    }
}
