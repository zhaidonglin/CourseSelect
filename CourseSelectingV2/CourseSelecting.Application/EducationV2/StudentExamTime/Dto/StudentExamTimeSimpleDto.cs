using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.StudentExamTime.Dto
{
    [AutoMapFrom(typeof(Education.StudentExamTime))]
    public class StudentExamTimeSimpleDto : EntityDto
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public virtual long StudentId { get; set; }

        /// <summary>
        /// 考试时间Id
        /// </summary>
        public virtual int ExamTimeId { get; set; }

        /// <summary>
        /// 考试分数
        /// </summary>
        public virtual double Credit { get; set; }
    }
}
