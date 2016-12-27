using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [Table("CsStudentExamTimes")]
    public class StudentExamTime : CreationAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual int ExamTimeId { get; set; }

        public virtual ExamTime ExamTime { get; set; }

        public virtual long StudentId { get; set; }

        public virtual Student Student { get; set; }

        /// <summary>
        /// 考试分数
        /// </summary>
        public virtual double Credit { get; set; }
    }
}
