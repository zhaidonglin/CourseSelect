namespace CourseSelecting.Application.EducationV2.Course.Dto
{
    public class CourseQueryInput : QueryInput
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public int? SubjectProjectId { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int? Id { get; set; }
    }
}
