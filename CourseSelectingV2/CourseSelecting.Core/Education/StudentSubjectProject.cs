using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [Table("CsStudentSubjectProjects")]
    public class StudentSubjectProject : CreationAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        //public virtual int SubjectProjectId { get; set; }

        /// <summary>
        /// 项目编号（应为SubjectProjectId）
        /// </summary>
        public virtual int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual SubjectProject SubjectProject { get; set; }

        public virtual long StudentId { get; set; }

        public virtual Student Student { get; set; }

        /// <summary>
        /// 课堂成绩
        /// </summary>
        public virtual double ClassCredit { get; set; }

        /// <summary>
        /// 考核成绩
        /// </summary>
        public virtual double ExamCredit { get; set; }

        /// <summary>
        /// 总成绩
        /// </summary>
        public virtual double TotalCadit { get; set; }
    }
}
