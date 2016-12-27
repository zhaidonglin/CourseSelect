using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [Table("CsTeacherCourses")]
    public class TeacherCourse : FullAuditedEntity<int>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual int CourseId { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public virtual Course Course { get; set; }

        public virtual long TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        /// <summary>
        /// 上课时间列表
        /// </summary>
        [ForeignKey("TeacherCourseId")]
        public virtual ICollection<CourseTime> CourseTimes { get; set; }

        /// <summary>
        /// 考试时间列表
        /// </summary>
        [ForeignKey("TeacherCourseId")]
        public virtual ICollection<ExamTime> ExamTimes { get; set; }
    }
}
