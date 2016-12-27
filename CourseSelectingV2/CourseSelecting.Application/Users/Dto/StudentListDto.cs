using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Users.Dto
{

    [AutoMapFrom(typeof(Student))]
    public class StudentListDto : EntityDto<long>
    {
        public string StudentNo { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
        public string Major { get; set; }
        public string Gender { get; set; }

        public string LoginId { get; set; }


        public string UserName { get; set; }

        public int CourseId { get; set; }           //10
    }
}