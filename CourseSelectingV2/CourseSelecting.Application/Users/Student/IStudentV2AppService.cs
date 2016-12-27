﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.Users.Student.Dto;

namespace CourseSelecting.Application.Users.Student
{
    public interface IStudentV2AppService : IApplicationService
    {
        /// <summary>
        /// 添加教师
        /// </summary>
        /// <param name="input">输入教师信息</param>
        /// <returns></returns>
        Task Add(StudentInput input);

        /// <summary>
        /// 编辑教师
        /// </summary>
        /// <param name="input">输入教师信息</param>
        /// <returns></returns>
        Task Edit(StudentInput input);

        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// 根据Id获取教师信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentDto> Get(int id);

        /// <summary>
        /// 根据输入参数获取教师信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentDto> Get(StudentQueryInput input);

        /// <summary>
        /// 根据id获取教师简要信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentSimpleDto> Simple(int id);

        /// <summary>
        /// 根据输入参数获取教师简要信息
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<StudentSimpleDto> Simple(StudentQueryInput input);

        /// <summary>
        /// 分页检索教师数据集
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        Task<PagedResultDto<StudentDto>> Query(StudentQueryInput input);
    }
}