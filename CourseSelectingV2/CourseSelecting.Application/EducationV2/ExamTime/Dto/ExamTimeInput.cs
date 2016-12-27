using System;

namespace CourseSelecting.Application.EducationV2.ExamTime.Dto
{
    public class ExamTimeInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 所属教师课程信息
        /// </summary>
        public int? TeacherCourseId { get; set; }
        
        /// <summary>
        /// 周次
        /// </summary>
        public int? Weeks { get; set; }

        /// <summary>
        /// 考试次数
        /// </summary>
        public int? Times { get; set; }

        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 截止预约时间
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 适合年级
        /// </summary>
        public string FitGrade { get; set; }

        /// <summary>
        /// 考试地点
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 主考老师
        /// </summary>
        public string Teacher { get; set; }
    }
}
