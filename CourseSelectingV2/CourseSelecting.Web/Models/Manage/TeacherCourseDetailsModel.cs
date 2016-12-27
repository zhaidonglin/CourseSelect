using Abp.Application.Services.Dto;
using CourseSelecting.Education.Dto;
using CourseSelecting.Users.Dto;

namespace CourseSelecting.Web.Models.Manage
{
    public class TeacherCourseDetailsModel
    {
        public TeacherDetailsDto Teacher { get; set; }

        public ListResultOutput<TeacherCourseDto> TeacherCourses { get; set; }

        public ListResultOutput<SubjectProjectCoursesDto> SubjectProjectCourses { get; set; }
    }
}