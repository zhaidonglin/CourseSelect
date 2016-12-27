using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.TeacherCourse.Dto;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.TeacherCourse
{
    public interface ITeacherCourseV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加教师课程
        /// </summary>
        /// <param name="input">输入教师课程信息</param>
        /// <returns></returns>
        Task Add(TeacherCourseInput input);

        /// <summary>
        /// 编辑教师课程
        /// </summary>
        /// <param name="input">输入教师课程信息</param>
        /// <returns></returns>
        Task Edit(TeacherCourseInput input);

        /// <summary>
        /// 删除教师课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取教师课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TeacherCourseDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取教师课程信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<TeacherCourseDto> Get(TeacherCourseQueryInput input);

        /// <summary>
        /// 根据id获取教师课程简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TeacherCourseSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取教师课程简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<TeacherCourseSimpleDto> Simple(TeacherCourseQueryInput input);

        /// <summary>
        /// 分页检索教师课程数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<TeacherCourseDto>> Query(TeacherCourseQueryInput input);

    }
}
