using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using System;

namespace CourseSelecting.Users
{
    [Table("CsStudents")]
    public class Student : User
    {
        public const int MaxStudentNoLength = 64;
        public const int MaxTelLength = 32;
        public const int MaxMajorLength = 128;
        public const int MaxGradeLength = 128;
        public const int MaxClassLength = 64;
        public const int MaxOriginOfStudentLength = 1024;
        public const int MaxDepartmentLength = 128;
        public const int MaxProfessionLevelLength = 64;


        public const string DefaultStudentPassword = "888888";

        /// <summary>
        /// 学号
        /// </summary>
        [StringLength(MaxStudentNoLength)]
        public virtual string StudentNo { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        public virtual int Gender { get; set; }

        /// <summary>
        /// 电话/联系方式
        /// </summary>
        [StringLength(MaxTelLength)]
        public virtual string Tel { get; set; }
        
        /// <summary>
        /// 专业
        /// </summary>
        [StringLength(MaxMajorLength)]
        public virtual string Major { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        [StringLength(MaxGradeLength)]
        public virtual string Grade { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        [StringLength(MaxClassLength)]
        public virtual string Class { get; set; }

        /// <summary>
        /// 入学时间
        /// </summary>
        public virtual DateTime EntryDate { get; set; }

        /// <summary>
        /// 生源地
        /// </summary>
        [StringLength(MaxOriginOfStudentLength)]
        public virtual string OriginOfStudent { get; set; }

        /// <summary>
        /// 院系
        /// </summary>
        [StringLength(MaxDepartmentLength)]
        public virtual string Department { get; set; }

        /// <summary>
        /// 专业等级
        /// </summary>
        [StringLength(MaxProfessionLevelLength)]
        public virtual string ProfessionLevel { get; set; }
    }
}
