using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseSelecting.Users
{
    [Table("CsTeachers")]
    public class Teacher : User
    {
        public const int MaxTeacherNoLength = 64;
        public const int MaxDepartmentLength = 128;
        public const int MaxMajorLength = 128;
        public const int MaxDiplomaLength = 32;
        public const int MaxDegreeLength = 32;
        public const int MaxPositionalTitleLength = 128;
        public const int MaxTelLength = 32;
        public const int MaxYearsOfWorkingLength = 64;
        public const int MaxYearsOfTeachingLength = 64;


        public const string DefaultTeacherPassword = "888888";

        /// <summary>
        /// 教师学号
        /// </summary>
        [StringLength(MaxTeacherNoLength)]
        public virtual string TeacherNo { get; set; }

        /// <summary>
        /// 院系
        /// </summary>
        [StringLength(MaxDepartmentLength)]
        public virtual string Department { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        public virtual int Gender { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        [StringLength(MaxMajorLength)]
        public virtual string Major { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [StringLength(MaxDiplomaLength)]
        public virtual string Diploma { get; set; }

        /// <summary>
        /// 学位
        /// </summary>
        [StringLength(MaxDegreeLength)]
        public virtual string Degree { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        [StringLength(MaxPositionalTitleLength)]
        public virtual string PositionalTitle { get; set; }

        /// <summary>
        /// 电话/联系方式
        /// </summary>
        [StringLength(MaxTelLength)]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 工作年限
        /// </summary>
        [StringLength(MaxYearsOfWorkingLength)]
        public virtual string YearsOfWorking { get; set; }

        /// <summary>
        /// 教学年限
        /// </summary>
        [StringLength(MaxYearsOfTeachingLength)]
        public virtual string YearsOfTeaching { get; set; }

        /// <summary>
        /// 是否是专职
        /// </summary>
        public virtual bool IsFullTime { get; set; }
    }
}
