using Abp.AutoMapper;
using CourseSelecting.Users.Dto;

namespace CourseSelecting.Users
{
    [AutoMapFrom(typeof(Education.Course))]
    public class CourseCalenderDto : Abp.Application.Services.Dto.EntityDto<int>
    {
        public CourseCalenderDetailsDto calender { get; set; }
    }
}