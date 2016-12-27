using Abp.AutoMapper;
using CourseSelecting.Users.Dto;

namespace CourseSelecting.Users
{
    [AutoMapFrom(typeof(Education.Course))]
    public class CalenderListDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }

        public TeacherDetailsDto Teacher { get; set; }

        public string Name { get; set; }

        public string Semester { get; set; }

        public string Credit { get; set; }
        public int LimitNumbers { get; set; }
        public int SelectedNumbers { get; set; }
    }
}