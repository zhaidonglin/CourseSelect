using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.Users.Student.Dto
{
    [AutoMapFrom(typeof(CourseSelecting.Users.Student))]
    public class StudentDto : EntityDto<long>
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNo { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 电话/联系方式
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 入学时间
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 生源地
        /// </summary>
        public string OriginOfStudent { get; set; }

        /// <summary>
        /// 院系
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 专业等级
        /// </summary>
        public string ProfessionLevel { get; set; }

        /// <summary>
        /// 学生学号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Surname { get; set; }
    }
}
