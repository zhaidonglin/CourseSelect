using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CourseSelecting.Education
{
    [Table("CsSubjectProjects")]
    public class SubjectProject : FullAuditedEntity<int>, IMayHaveTenant
    {
        public const int MaxNameLength = 128;
        public const int MaxSubjectStyleLength = 64;
        public const int MaxTypeLength = 64;
        public const int MaxAimedAtLength = 64;
        public const int MaxTeachingStyleLength = 64;
        public const int MaxDiscriptionLength = 2048;

        public int? TenantId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required, StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目模式
        /// </summary>
        [StringLength(MaxSubjectStyleLength)]
        public virtual string SubjectStyle { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [StringLength(MaxTypeLength)]
        public virtual string Type { get; set; }
        
        /// <summary>
        /// 学分
        /// </summary>
        public virtual double Credit { get; set; }

        /// <summary>
        /// 项目对象
        /// </summary>
        [StringLength(MaxAimedAtLength)]
        public virtual string AimedAt { get; set; }

        /// <summary>
        /// 是否是必修
        /// </summary>
        public virtual bool IsCompulsory { get; set; }

        /// <summary>
        /// 授课模式
        /// </summary>
        [StringLength(MaxTeachingStyleLength)]
        public virtual string TeachingStyle { get; set; }

        /// <summary>
        /// 项目介绍
        /// </summary>
        [StringLength(MaxDiscriptionLength)]
        public virtual string Discription { get; set; }

        /// <summary>
        /// 所属学期编号
        /// </summary>
        public virtual int SemesterId { get; set; }

        /// <summary>
        /// 所属学期
        /// </summary>
        public virtual Semester Semester { get; set; }

        /// <summary>
        /// 课程列表
        /// </summary>
        [ForeignKey("SubjectProjectId")]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
