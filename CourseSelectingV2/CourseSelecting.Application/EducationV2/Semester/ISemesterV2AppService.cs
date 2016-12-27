using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.Course.Dto;
using CourseSelecting.Application.EducationV2.Semester.Dto;

namespace CourseSelecting.Application.EducationV2.Semester
{
    public interface ISemesterV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加学期
        /// </summary>
        /// <param name="input">输入学期信息</param>
        /// <returns></returns>
        Task Add(SemesterInput input);

        /// <summary>
        /// 编辑学期
        /// </summary>
        /// <param name="input">输入学期信息</param>
        /// <returns></returns>
        Task Edit(SemesterInput input);

        /// <summary>
        /// 删除学期
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取学期信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SemesterDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取学期信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<SemesterDto> Get(SemesterQueryInput input);

        /// <summary>
        /// 根据id获取学期简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SemesterSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取学期简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<SemesterSimpleDto> Simple(SemesterQueryInput input);

        /// <summary>
        /// 分页检索学期数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<SemesterDto>> Query(SemesterQueryInput input);
    }
}
