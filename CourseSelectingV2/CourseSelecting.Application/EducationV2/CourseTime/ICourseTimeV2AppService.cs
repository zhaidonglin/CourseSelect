using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.CourseTime.Dto;

namespace CourseSelecting.Application.EducationV2.CourseTime
{
    public interface ICourseTimeV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加上课时间
        /// </summary>
        /// <param name="input">输入上课时间信息</param>
        /// <returns></returns>
        Task Add(CourseTimeInput input);

        /// <summary>
        /// 编辑上课时间
        /// </summary>
        /// <param name="input">输入上课时间信息</param>
        /// <returns></returns>
        Task Edit(CourseTimeInput input);

        /// <summary>
        /// 删除上课时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取上课时间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseTimeDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取上课时间信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<CourseTimeDto> Get(CourseTimeQueryInput input);

        /// <summary>
        /// 根据id获取上课时间简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CourseTimeSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取上课时间简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<CourseTimeSimpleDto> Simple(CourseTimeQueryInput input);

        /// <summary>
        /// 分页检索上课时间数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<CourseTimeDto>> Query(CourseTimeQueryInput input);
    }
}
