using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CourseSelecting.Application.EducationV2.Course.Dto;
using CourseSelecting.Application.EducationV2.Semester.Dto;
using CourseSelecting.Application.Users.Teacher.Dto;


namespace CourseSelecting.Application.EducationV2.SubjectProject.Dto
{
    [AutoMapFrom(typeof(Education.SubjectProject))]
    public class SubjectProjectDto : EntityDto
    {
        /// <summary>
        /// 所属教师
        /// </summary>
        public TeacherDto Teacher { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目模式
        /// </summary>
        public string SubjectStyle { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public double Credit { get; set; }
        /// <summary>
        /// 项目对象
        /// </summary>
        public string AimedAt { get; set; }

        /// <summary>
        /// 是否是必修
        /// </summary>
        public bool IsCompulsory { get; set; }

        /// <summary>
        /// 授课模式
        /// </summary>
        public string TeachingStyle { get; set; }

        /// <summary>
        /// 项目介绍
        /// </summary>
        public string Discription { get; set; }
        /// <summary>
        /// 所属学期编号
        /// </summary>
        public  int SemesterId { get; set; }

        /// <summary>
        /// 所属学期
        /// </summary>
        public SemesterDto Semester { get; set; }
        /// <summary>
        /// 课程列表
        /// </summary>
        public CourseDto Course { get; set; }
    }
}
