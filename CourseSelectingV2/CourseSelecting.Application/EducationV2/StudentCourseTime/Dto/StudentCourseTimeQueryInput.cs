namespace CourseSelecting.Application.EducationV2.StudentCourseTime.Dto
{
    public class StudentCourseTimeQueryInput : QueryInput
    {
        /// <summary>
        /// 学生预约课程关系编号
        /// </summary>
        public int? Id { get; set; }

        public int? CourseTimeId { get; set; }

        /// <summary>
        /// 学生所选项目编号
        /// </summary>
        public int? StudentSubjectProjectId { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int? CourseId { get; set; }

    }
}
