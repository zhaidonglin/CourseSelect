using Abp.AutoMapper;
using CourseSelecting.Users.Dto;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(TeacherCourse))]
    public class TeacherCourseDto
    {
        public int Id { get; set; }
        public CourseDetailsDto Course { get; set; }
        public TeacherSimpleDetailsDto Teacher { get; set; }
    }
}
