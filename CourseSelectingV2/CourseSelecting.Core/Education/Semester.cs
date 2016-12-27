using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CourseSelecting.Education
{
    [Table("CsSemesters")]
    public class Semester : FullAuditedEntity<int>, IMayHaveTenant
    {
        public const int MaxNameLength = 128;

        /// <summary>
        /// 系统租户
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 学期
        /// </summary>
        [Required, StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 学期开始时间
        /// </summary>
        public virtual DateTime Start { get; set; }

        /// <summary>
        /// 学期结束时间
        /// </summary>
        public virtual DateTime End { get; set; }
    }
}
