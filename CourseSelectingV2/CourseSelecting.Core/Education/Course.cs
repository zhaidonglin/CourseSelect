using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [Table("CsCourses")]
    public class Course : FullAuditedEntity<int>, IMayHaveTenant
    {
        public const int MaxNameLength = 128;
        public const int MaxRemarkLength = 2048;

        /// <summary>
        /// 系统租户
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public int SubjectProjectId { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        [ForeignKey("SubjectProjectId")]
        public virtual SubjectProject SubjectProject { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [Required, StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public virtual double Credit { get; set; }

        /// <summary>
        /// 限选人数
        /// </summary>
        public virtual int LimitNumbers { get; set; }

        /// <summary>
        /// 已选人数
        /// </summary>
        public virtual int SelectedNumbers { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaxRemarkLength)]
        public virtual string Remark { get; set; }
    }
}
