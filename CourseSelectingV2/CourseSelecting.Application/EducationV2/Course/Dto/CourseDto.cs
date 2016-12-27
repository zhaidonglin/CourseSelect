using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.SubjectProject.Dto;

namespace CourseSelecting.Application.EducationV2.Course.Dto
{
    [AutoMapFrom(typeof(Education.Course))]
    public class CourseDto : EntityDto
    {
        /// <summary>
        /// 所属项目
        /// </summary>
        public SubjectProjectDto SubjectProject { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
