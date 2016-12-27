using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.SubjectProject.Dto;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.SubjectProject
{
    public interface ISubjectProjectV2AppService
    {

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="input">输入课程信息</param>
        /// <returns></returns>
        Task Add(SubjectProjectInput input);

        /// <summary>
        /// 编辑课程
        /// </summary>
        /// <param name="input">输入课程信息</param>
        /// <returns></returns>
        Task Edit(SubjectProjectInput input);

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
        Task<SubjectProjectDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取课程信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<SubjectProjectDto> Get(SubjectProjectQueryInput input);

        /// <summary>
        /// 根据id获取课程简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SubjectProjectSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取课程简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<SubjectProjectSimpleDto> Simple(SubjectProjectQueryInput input);

        /// <summary>
        /// 分页检索课程数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<SubjectProjectDto>> Query(SubjectProjectQueryInput input);
    }
}
