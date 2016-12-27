using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.ExamTime.Dto;
using CourseSelecting.Application.Users.Student.Dto;

namespace CourseSelecting.Application.EducationV2.StudentExamTime.Dto
{
    [AutoMapFrom(typeof(Education.StudentExamTime))]
    public class StudentExamTimeDto : EntityDto
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public StudentDto Student { get; set; }

        /// <summary>
        /// 所选的考试时间信息
        /// </summary>
        public virtual ExamTimeDto ExamTime { get; set; }

        /// <summary>
        /// 考试分数
        /// </summary>
        public double Credit { get; set; }
    }
}
