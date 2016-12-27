namespace CourseSelecting.Application.EducationV2.Course.Dto
{
    public class CourseInput
    {
        public int? Id { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public int? SubjectProjectId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public double? Credit { get; set; }

        /// <summary>
        /// 限选人数
        /// </summary>
        public int? LimitNumbers { get; set; }

        /// <summary>
        /// 已选人数
        /// </summary>
        public int? SelectedNumbers { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
