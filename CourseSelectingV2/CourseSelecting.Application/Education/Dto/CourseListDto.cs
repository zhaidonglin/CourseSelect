using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(Course))]
    public class CourseListDto
    {
        public int Id { get; set; }

        public SubjectProjectSimpleDto SubjectProject { get; set; }

        public string Name { get; set; }

        public string Credit { get; set; }

        public int LimitNumbers { get; set; }

        public int SelectedNumbers { get; set; }
    }
}
