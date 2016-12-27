namespace CourseSelecting.Application.EducationV2.StudentSubjectProject.Dto
{
    public class StudentSubjectProjectInput
    {
        public int? Id { get; set; }

        /// <summary>
        /// 项目编号（应为SubjectProjectId）
        /// </summary>
        public int? CourseId { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public long? StudentId { get; set; }
        
        /// <summary>
        /// 课堂成绩
        /// </summary>
        public double? ClassCredit { get; set; }

        /// <summary>
        /// 考核成绩
        /// </summary>
        public double? ExamCredit { get; set; }

        /// <summary>
        /// 总成绩
        /// </summary>
        public double? TotalCadit { get; set; }
    }
}
