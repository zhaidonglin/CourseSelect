using System;

namespace CourseSelecting.Application.EducationV2.Semester.Dto
{
    public class SemesterInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 学期
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学期开始时间
        /// </summary>
        public DateTime? Start { get; set; }

        /// <summary>
        /// 学期结束时间
        /// </summary>
        public DateTime? End { get; set; }
    }
}
