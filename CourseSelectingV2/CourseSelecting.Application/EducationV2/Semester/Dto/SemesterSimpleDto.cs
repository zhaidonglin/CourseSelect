using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.Semester.Dto
{
    [AutoMapFrom(typeof(Education.Semester))]
    public class SemesterSimpleDto: EntityDto
    {
        /// <summary>
        /// 学期名称
        /// </summary>
        public string Name { get; set; }
    }
}
