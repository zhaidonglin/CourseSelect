using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CourseSelecting.Application.EducationV2.StudentSubjectProject.Dto;
using Abp.UI;
using Abp.AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace CourseSelecting.Application.EducationV2.StudentSubjectProject
{
    public class StudentSubjectProjectV2AppService : CourseSelectingAppServiceBase, IStudentSubjectProjectV2AppService
    {
        private readonly IRepository<Education.StudentSubjectProject> _studentSubjectProjectRepository;

        public StudentSubjectProjectV2AppService(
            IRepository<Education.StudentSubjectProject> studentSubjectProjectRepository)
        {
            _studentSubjectProjectRepository = studentSubjectProjectRepository;
        }

        public async Task Add(StudentSubjectProjectInput input)
        {
            //检查Id参数
            if (!input.CourseId.HasValue) throw new UserFriendlyException("传入CourseId参数不正确！");
            if (!input.StudentId.HasValue) throw new UserFriendlyException("传入StudentId参数不正确！");

            //验证学生项目所属项目是否存在
            var subject = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.CourseId);
            if (subject == null) throw new UserFriendlyException($"编号为{input.CourseId}的项目不存在！");

            //创建学生项目对象
            var studentSubjectProject = new Education.StudentSubjectProject
            {
                CourseId = input.CourseId.Value,
                StudentId = input.StudentId.Value
            };

            //课堂成绩属性赋值
            if (input.ClassCredit.HasValue) studentSubjectProject.ClassCredit = input.ClassCredit.Value;
            //考核成绩属性赋值
            if (input.ExamCredit.HasValue) studentSubjectProject.ExamCredit = input.ExamCredit.Value;
            // 总成绩属性赋值
            if (input.TotalCadit.HasValue) studentSubjectProject.TotalCadit = input.TotalCadit.Value;

            await _studentSubjectProjectRepository.InsertAsync(studentSubjectProject);
        }

        public async Task Delete(int id)
        {
            await _studentSubjectProjectRepository.DeleteAsync(id);
        }

        public async Task Edit(StudentSubjectProjectInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //验证
            var course = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.CourseId);

            if (course == null) throw new UserFriendlyException($"编号为{input.CourseId}的课程不存在！");

            //获取需要修改的对象
            var studentSubjectProject = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);

            if (studentSubjectProject == null) throw new UserFriendlyException("当前课程不存在！");

            //修改属性值
            if (input.CourseId.HasValue) studentSubjectProject.CourseId = input.CourseId.Value;
            if (input.StudentId.HasValue) studentSubjectProject.StudentId = input.StudentId.Value;
            //课堂成绩属性赋值
            if (input.ClassCredit.HasValue) studentSubjectProject.ClassCredit = input.ClassCredit.Value;
            //考核成绩属性赋值
            if (input.ExamCredit.HasValue) studentSubjectProject.ExamCredit = input.ExamCredit.Value;
            // 总成绩属性赋值
            if (input.TotalCadit.HasValue) studentSubjectProject.TotalCadit = input.TotalCadit.Value;

            //执行修改数据方法
            await _studentSubjectProjectRepository.UpdateAsync(studentSubjectProject);
        }

        public async Task<StudentSubjectProjectDto> Get(StudentSubjectProjectQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSubjectProjectDto>();
        }

        public async Task<StudentSubjectProjectDto> Get(int id)
        {
            var result = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSubjectProjectDto>();
        }
        public async Task<StudentSubjectProjectSimpleDto> Simple(int id)
        {
            var result = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSubjectProjectSimpleDto>();
        }
        public async Task<PagedResultDto<StudentSubjectProjectDto>> Query(StudentSubjectProjectQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取总数
            var totalcount = await _studentSubjectProjectRepository.CountAsync();

            //获取查询对象
            var query = _studentSubjectProjectRepository.GetAll();

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.SubjectProject.Name.Contains(input.KeyWord)
                );
            }

            ////添加所属项目条件
            //if (input.SubjectProjectId.HasValue)
            //{
            //    query = query.Where(x => x.SubjectProjectId == input.SubjectProjectId.Value);
            //}

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<StudentSubjectProjectDto>(totalcount, courses.MapTo<List<StudentSubjectProjectDto>>());
        }

        public async Task<StudentSubjectProjectSimpleDto> Simple(StudentSubjectProjectQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSubjectProjectSimpleDto>();
        }

       
    }
}
