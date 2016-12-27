using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Users.Dto
{
    [AutoMapFrom(typeof(Teacher))]
    public class TeacherListDto : EntityDto<long>
    {
        public string TeacherNo { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public string UserName { get; set; }

        ///// <summary>
        ///// 个人资料
        ///// </summary>
        //public int Gender { get; set; }
        //public string Major { get; set; }
        //public string Diploma { get; set; }
        //public string Degree { get; set; }
        //public string PositionalTitle { get; set; }
        //public string YearsOfWorking { get; set; }
        //public string YearsOfTeaching { get; set; }
        //public bool IsFullTime { get; set; }
    }
}
