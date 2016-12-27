using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.Course.Dto;

namespace CourseSelecting.Application.EducationV2.Course
{
    public interface ICourseV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="input">输入课程信息</param>
        /// <returns></returns>
        Task Add(CourseInput input);

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="input">输入课程信息</param>
        /// <returns>返回新添加的对象，包含主键</returns>
        Task<CourseDto> AddAndGetObj(CourseInput input);

        /// <summary>
        /// 编辑课程
        /// </summary>
        /// <param name="input">输入课程信息</param>
        /// <returns></returns>
        Task Edit(CourseInput input);

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取课程信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<CourseDto> Get(CourseQueryInput input);

        /// <summary>
        /// 根据id获取课程简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取课程简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<CourseSimpleDto> Simple(CourseQueryInput input);

        /// <summary>
        /// 分页检索课程数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<CourseDto>> Query(CourseQueryInput input);
    }
}
