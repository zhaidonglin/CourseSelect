using Abp.Application.Services.Dto;
using CourseSelecting.Education.Dto;

namespace CourseSelecting.Web.Models.Manage
{
    public class CourseDetailsModel
    {
        public ListResultOutput<SubjectProjectSimpleDto> SubjectProjects { get; set; }
        public CourseDetailsDto Course { get; set; }
    }
}