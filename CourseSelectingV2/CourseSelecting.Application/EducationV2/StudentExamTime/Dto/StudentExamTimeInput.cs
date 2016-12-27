using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.StudentExamTime.Dto
{
    public class StudentExamTimeInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 考试时间编号
        /// </summary>
        public int? ExamTimeId { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public long? StudentId { get; set; }

        /// <summary>
        /// 考试分数
        /// </summary>
        public double? Credit { get; set; }
    }
}
