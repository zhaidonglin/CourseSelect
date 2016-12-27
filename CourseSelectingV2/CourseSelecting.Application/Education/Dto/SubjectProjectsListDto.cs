using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(SubjectProject))]
    public class SubjectProjectsListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SubjectStyle { get; set; }

        public string Type { get; set; }

        public double Credit { get; set; }

        public string AimedAt { get; set; }

        public bool IsCompulsory { get; set; }

        public string TeachingStyle { get; set; }

        public SemesterDto Semester { get; set; }

    }
}
