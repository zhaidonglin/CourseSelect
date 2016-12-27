namespace CourseSelecting.Application.EducationV2.StudentCourseTime.Dto
{
    public class StudentCourseTimeInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 所选上课时间
        /// </summary>
        public int? CourseTimeId { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public long? StudentId { get; set; }

        /// <summary>
        /// 随堂分数
        /// </summary>
        public double? ClassCredit { get; set; }
    }
}
