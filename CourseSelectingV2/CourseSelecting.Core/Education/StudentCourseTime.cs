using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [Table("CsStudentCourseTimes")]
    public class StudentCourseTime : CreationAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual int CourseTimeId { get; set; }

        public virtual CourseTime CourseTime { get; set; }

        public virtual long StudentId { get; set; }

        public virtual Student Student { get; set; }

        /// <summary>
        /// 随堂分数
        /// </summary>
        public virtual double ClassCredit { get; set; }
    }
}
