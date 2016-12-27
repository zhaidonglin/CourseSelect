using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Users.Dto
{

    [AutoMapFrom(typeof(Teacher))]
    public class TeacherSimpleDetailsDto : EntityDto
    {
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string TeacherNo { get; set; }
    }
}
