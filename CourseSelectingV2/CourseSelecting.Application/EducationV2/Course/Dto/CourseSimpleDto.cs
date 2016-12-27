using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.EducationV2.Course.Dto
{
    [AutoMapFrom(typeof(Education.Course))]
    public class CourseSimpleDto : EntityDto
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public double Credit { get; set; }

        /// <summary>
        /// 限选人数
        /// </summary>
        public int LimitNumbers { get; set; }

        /// <summary>
        /// 已选人数
        /// </summary>
        public int SelectedNumbers { get; set; }
    }
}
