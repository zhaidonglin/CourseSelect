using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(Course))]
    public class CourseSimpleListDto:EntityDto
    {
        public string Name { get; set; }

        public double Credit { get; set; }

        public int LimitNumbers { get; set; }
    }
}
