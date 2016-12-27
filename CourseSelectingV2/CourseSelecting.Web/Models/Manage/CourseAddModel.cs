using Abp.Application.Services.Dto;
using CourseSelecting.Education.Dto;

namespace CourseSelecting.Web.Models.Manage
{
    public class CourseAddModel
    {
        public ListResultOutput<SubjectProjectSimpleDto> SubjectProjects { get; set; }
        public ListResultOutput<CourseTimeDto> CourseTimes { get; set; }
    }
}