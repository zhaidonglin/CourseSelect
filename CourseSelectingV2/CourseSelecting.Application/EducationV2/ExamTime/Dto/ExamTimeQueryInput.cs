using System;

namespace CourseSelecting.Application.EducationV2.ExamTime.Dto
{
    public class ExamTimeQueryInput : QueryInput
    {
        public int? Id { get; set; }

        /// <summary>
        /// 上课时间满足的时间范围的起始时间
        /// </summary>
        public DateTime? BeginDateTime { get; set; }

        /// <summary>
        /// 上课时间满足的时间范围的结束时间
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// 发布考试信息的教师编号
        /// </summary>
        public long? TeacherId { get; set; }
    }
}
