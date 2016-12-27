using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(SubjectProject))]
    public class SubjectProjectSimpleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsCompulsory { get; set; }
    }
}
