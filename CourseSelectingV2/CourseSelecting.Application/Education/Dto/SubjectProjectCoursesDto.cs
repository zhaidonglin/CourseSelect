using System.Collections.Generic;
using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(SubjectProject))]
    public class SubjectProjectCoursesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SemesterDto Semester { get; set; }

        public List<CourseSimpleListDto> Courses { get; set; } = new List<CourseSimpleListDto>();
    }
}
