using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.StudentSubjectProject.Dto;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.StudentSubjectProject
{
    public interface IStudentSubjectProjectV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加学生项目
        /// </summary>
        /// <param name="input">输入项目信息</param>
        /// <returns></returns>
        Task Add(StudentSubjectProjectInput input);

        /// <summary>
        /// 编辑项目
        /// </summary>
        /// <param name="input">输入项目信息</param>
        /// <returns></returns>
        Task Edit(StudentSubjectProjectInput input);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentSubjectProjectDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取项目信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentSubjectProjectDto> Get(StudentSubjectProjectQueryInput input);

        /// <summary>
        /// 根据id获取项目简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentSubjectProjectSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取项目简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentSubjectProjectSimpleDto> Simple(StudentSubjectProjectQueryInput input);

        /// <summary>
        /// 分页检索项目数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<StudentSubjectProjectDto>> Query(StudentSubjectProjectQueryInput input);
    }
}
