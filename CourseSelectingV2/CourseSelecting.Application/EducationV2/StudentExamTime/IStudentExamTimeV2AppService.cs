using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.StudentExamTime.Dto;

namespace CourseSelecting.Application.EducationV2.StudentExamTime
{
    public interface IStudentExamTimeV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加学生预约考试
        /// </summary>
        /// <param name="input">输入学生预约考试信息</param>
        /// <returns></returns>
        Task Add(StudentExamTimeInput input);

        /// <summary>
        /// 编辑学生预约考试
        /// </summary>
        /// <param name="input">输入学生预约考试信息</param>
        /// <returns></returns>
        Task Edit(StudentExamTimeInput input);

        /// <summary>
        /// 删除学生预约考试
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取学生预约考试信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentExamTimeDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取学生预约考试信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentExamTimeDto> Get(StudentExamTimeQueryInput input);

        /// <summary>
        /// 根据id获取学生预约考试简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentExamTimeSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取学生预约考试简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentExamTimeSimpleDto> Simple(StudentExamTimeQueryInput input);

        /// <summary>
        /// 分页检索学生预约考试数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<StudentExamTimeDto>> Query(StudentExamTimeQueryInput input);
    }
}
