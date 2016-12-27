using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CourseSelecting.Application.Users.Teacher.Dto
{
    [AutoMapFrom(typeof(CourseSelecting.Users.Teacher))]
    public class TeacherSimpleDto : EntityDto<long>
    {
        /// <summary>
        /// 教师学号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 院系
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 登录Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string Surname { get; set; }
    }
}
