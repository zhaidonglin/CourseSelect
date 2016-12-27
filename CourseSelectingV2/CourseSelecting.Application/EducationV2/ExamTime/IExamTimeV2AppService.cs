using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.ExamTime.Dto;

namespace CourseSelecting.Application.EducationV2.ExamTime
{
    public interface IExamTimeV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加考试时间
        /// </summary>
        /// <param name="input">输入考试时间信息</param>
        /// <returns></returns>
        Task Add(ExamTimeInput input);
        /// <summary>
        /// 添加考试时间
        /// </summary>
        /// <param name="input">输入考试时间信息</param>
        /// <returns></returns>
        Task<ExamTimeDto> AddAndGetObj(ExamTimeInput input);

        /// <summary>
        /// 编辑考试时间
        /// </summary>
        /// <param name="input">输入考试时间信息</param>
        /// <returns></returns>
        Task Edit(ExamTimeInput input);

        /// <summary>
        /// 删除考试时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取考试时间信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExamTimeDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取考试时间信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<ExamTimeDto> Get(ExamTimeQueryInput input);

        /// <summary>
        /// 根据id获取考试时间简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExamTimeSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取考试时间简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<ExamTimeSimpleDto> Simple(ExamTimeQueryInput input);

        /// <summary>
        /// 分页检索考试时间数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<ExamTimeDto>> Query(ExamTimeQueryInput input);
    }
}
