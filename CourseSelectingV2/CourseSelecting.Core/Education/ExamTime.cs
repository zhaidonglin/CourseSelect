using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CourseSelecting.Education
{
    [Table("CsExamTimes")]
    public class ExamTime : CreationAuditedEntity<int>, IMayHaveTenant
    {
        public const int MaxAddressLength = 521;
        public const int MaxTeacherLength = 128;
        public const int MaxFitGradeLength = 521;

        public int? TenantId { get; set; }

        public int TeacherCourseId { get; set; }

        /// <summary>
        /// 所属教师课程信息
        /// </summary>
        [ForeignKey("TeacherCourseId")]
        public virtual TeacherCourse TeacherCourse { get; set; }

        /// <summary>
        /// 周次
        /// </summary>
        public virtual int Weeks { get; set; }

        /// <summary>
        /// 考试次数
        /// </summary>
        public virtual int Times { get; set; }

        /// <summary>
        /// 考试时间
        /// </summary>
        public virtual DateTime Start { get; set; }

        /// <summary>
        /// 截止预约时间
        /// </summary>
        public virtual DateTime End { get; set; }

        /// <summary>
        /// 适合年级
        /// </summary>
        [StringLength(MaxFitGradeLength)]
        public virtual string FitGrade { get; set; }

        /// <summary>
        /// 考试地点
        /// </summary>
        [StringLength(MaxAddressLength)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 主考老师
        /// </summary>
        [StringLength(MaxTeacherLength)]
        public virtual string Teacher { get; set; }
    }
}
