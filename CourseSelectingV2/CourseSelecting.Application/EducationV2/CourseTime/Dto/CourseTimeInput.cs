using System;

namespace CourseSelecting.Application.EducationV2.CourseTime.Dto
{
    public class CourseTimeInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 所属教师课程编号
        /// </summary>
        public int? TeacherCourseId { get; set; }

        /// <summary>
        /// 周次
        /// </summary>
        public int? Weeks { get; set; }

        /// <summary>
        /// 课程次数
        /// </summary>
        public int? Times { get; set; }

        /// <summary>
        /// 上课时间
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? End { get; set; }

        /// <summary>
        /// 适合年级
        /// </summary>
        public string FitGrade { get; set; }

        /// <summary>
        /// 上课地点
        /// </summary>
        public string Address { get; set; }
    }
}
