using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.SubjectProject.Dto;
using CourseSelecting.Application.Users.Student.Dto;

namespace CourseSelecting.Application.EducationV2.StudentSubjectProject.Dto
{
    [AutoMapFrom(typeof(Education.StudentSubjectProject))]
    public class StudentSubjectProjectDto : EntityDto
    {

        /// <summary>
        /// 项目信息
        /// </summary>
        public SubjectProjectDto SubjectProject { get; set; }
        
        /// <summary>
        /// 学生信息
        /// </summary>
        public StudentDto Student { get; set; }

        /// <summary>
        /// 课堂成绩
        /// </summary>
        public double? ClassCredit { get; set; }

        /// <summary>
        /// 考核成绩
        /// </summary>
        public double? ExamCredit { get; set; }

        /// <summary>
        /// 总成绩
        /// </summary>
        public double? TotalCadit { get; set; }
    }
}
