using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CourseSelecting.Application.EducationV2.TeacherCourse.Dto;
using Abp.UI;
using Abp.AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace CourseSelecting.Application.EducationV2.TeacherCourse
{
    public class TeacherCourseV2AppService : CourseSelectingAppServiceBase, ITeacherCourseV2AppService
    {
        private readonly IRepository<Education.TeacherCourse> _teacherCourseRepository;

        public TeacherCourseV2AppService(
            IRepository<Education.TeacherCourse> teacherCourseRepository)
        {
            _teacherCourseRepository = teacherCourseRepository;
        }

        public async Task Add(TeacherCourseInput input)
        {
            //检查Id参数
            if (!input.CourseId.HasValue) throw new UserFriendlyException("传入CourseId参数不正确！");
            if (!input.TeacherId.HasValue) throw new UserFriendlyException("传入TeacherId参数不正确！");

            //验证
            var course = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.CourseId);
            if (course == null) throw new UserFriendlyException($"编号为{input.CourseId}的课程不存在！");

            //创建教师课程对象
            var teacherCourse = new Education.TeacherCourse
            {
                CourseId = input.CourseId.Value,
                TeacherId = input.TeacherId.Value
            };

            await _teacherCourseRepository.InsertAsync(teacherCourse);
        }

        public async Task Delete(int id)
        {
            await _teacherCourseRepository.DeleteAsync(id);
        }

        public async Task Edit(TeacherCourseInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //验证
            var course = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.CourseId);

            if (course == null) throw new UserFriendlyException($"编号为{input.CourseId}的课程不存在！");

            //获取需要修改的对象
            var teacherCourse = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);

            if (teacherCourse == null) throw new UserFriendlyException("当前课程不存在！");

            //修改属性值
            if (input.CourseId.HasValue) teacherCourse.CourseId = input.CourseId.Value;
            if (input.TeacherId.HasValue) teacherCourse.TeacherId = input.TeacherId.Value;


            //执行修改数据方法
            await _teacherCourseRepository.UpdateAsync(teacherCourse);
        }

        public async Task<TeacherCourseDto> Get(TeacherCourseQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherCourseDto>();
        }

        public async Task<TeacherCourseDto> Get(int id)
        {
            var result = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherCourseDto>();
        }

        public async Task<PagedResultDto<TeacherCourseDto>> Query(TeacherCourseQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");


            //获取查询对象
            var query = _teacherCourseRepository.GetAll();

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Course.Name.Contains(input.KeyWord)
                );
            }

            //添加所属项目条件
            if (input.TeacherId.HasValue)
            {
                query = query.Where(x => x.TeacherId == input.TeacherId.Value);
            }

            //获取总数
            var totalcount = await Task.FromResult(query.Count());

            //添加分页条件
            if (input.PageSize.Value > 0)
            {
                query = query.OrderBy(x => x.CreationTime)
                    .Skip(input.Start.Value).Take(input.PageSize.Value);
            }

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<TeacherCourseDto>(totalcount, courses.MapTo<List<TeacherCourseDto>>());
        }

        public async Task<TeacherCourseSimpleDto> Simple(TeacherCourseQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherCourseSimpleDto>();
        }

        public async Task<TeacherCourseSimpleDto> Simple(int id)
        {
            var result = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherCourseSimpleDto>();
        }
    }
}
