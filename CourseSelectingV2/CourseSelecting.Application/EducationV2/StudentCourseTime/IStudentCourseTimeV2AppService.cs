using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.StudentCourseTime.Dto;

namespace CourseSelecting.Application.EducationV2.StudentCourseTime
{
    public interface IStudentCourseTimeV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加学生预约课程
        /// </summary>
        /// <param name="input">输入学生预约课程信息</param>
        /// <returns></returns>
        Task Add(StudentCourseTimeInput input);

        /// <summary>
        /// 编辑学生预约课程
        /// </summary>
        /// <param name="input">输入学生预约课程信息</param>
        /// <returns></returns>
        Task Edit(StudentCourseTimeInput input);

        /// <summary>
        /// 删除学生预约课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取学生预约课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentCourseTimeDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取学生预约课程信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentCourseTimeDto> Get(StudentCourseTimeQueryInput input);

        /// <summary>
        /// 根据id获取学生预约课程简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentCourseTimeSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取学生预约课程简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentCourseTimeSimpleDto> Simple(StudentCourseTimeQueryInput input);

        /// <summary>
        /// 分页检索学生预约课程数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<StudentCourseTimeDto>> Query(StudentCourseTimeQueryInput input);
    }
}
