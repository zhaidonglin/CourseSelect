using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Application.Users.Student.Dto
{
    public class StudentSimpleDto
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
        /// 入学时间
        /// </summary>
        public DateTime EntryDate { get; set; }

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
