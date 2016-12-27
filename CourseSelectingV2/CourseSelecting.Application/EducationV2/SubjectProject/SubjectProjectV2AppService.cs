using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.EducationV2.SubjectProject.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace CourseSelecting.Application.EducationV2.SubjectProject
{
    public class SubjectProjectV2AppService : CourseSelectingAppServiceBase, ISubjectProjectV2AppService
    {
        private readonly IRepository<Education.SubjectProject> _subjectProjectRepository;
        private readonly IRepository<Education.Semester> _semesterRepository;

        public SubjectProjectV2AppService(
            IRepository<Education.SubjectProject> subjectProjectRepository,
            IRepository<Education.Semester> semesterRepository
            )
        {
            _subjectProjectRepository = subjectProjectRepository;
            _semesterRepository = semesterRepository;
        }
        public async Task Add(SubjectProjectInput input)
        {
            //验证传入参数
            if (string.IsNullOrEmpty(input.Name)) throw new UserFriendlyException("传入Name参数不正确！");
            if (string.IsNullOrEmpty(input.SubjectStyle)) throw new UserFriendlyException("传入SubjectStyle参数不正确！");
            if (string.IsNullOrEmpty(input.Type)) throw new UserFriendlyException("传入Type参数不正确！");
            if (!input.Credit.HasValue) throw new UserFriendlyException("传入Credit参数不正确！");
            if (string.IsNullOrEmpty(input.AimedAt)) throw new UserFriendlyException("传入AimedAt参数不正确！");
            if (!input.IsCompulsory.HasValue) throw new UserFriendlyException("传入IsCompulsory参数不正确！");
            if (string.IsNullOrEmpty(input.TeachingStyle)) throw new UserFriendlyException("传入TeachingStyle参数不正确！");
            if(string.IsNullOrEmpty(input.Discription)) throw new UserFriendlyException("传入Discription参数不正确！");
            if (!input.SemesterId.HasValue) throw new UserFriendlyException("传入SemesterId参数不正确！");

            //验证项目所属学期是否存在
            var semester = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == input.SemesterId);
            if (semester == null) throw new UserFriendlyException($"所属学期编号为{input.SemesterId}的项目不存在！");

            //创建项目对象
            var subjectProject = new Education.SubjectProject
            {
                Name = input.Name,
                SubjectStyle = input.SubjectStyle,
                Type = input.Type,
                Credit = input.Credit.Value,
                AimedAt = input.AimedAt,
                IsCompulsory = input.IsCompulsory.Value,
                TeachingStyle = input.TeachingStyle,
                Discription = input.Discription,
                SemesterId = input.SemesterId.Value
            };

            // 项目介绍属性赋值
            if (!string.IsNullOrWhiteSpace(input.Discription)) subjectProject.Discription = input.Discription;

            //执行插入数据方法
            await _subjectProjectRepository.InsertAsync(subjectProject);
        }

        public async Task Delete(int id)
        {
            await _subjectProjectRepository.DeleteAsync(id);
        }

        public async Task Edit(SubjectProjectInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //获取需要修改的对象
            var subjectProject = await _subjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (subjectProject == null) throw new UserFriendlyException("当前项目不存在！");

            //修改属性值
            if (!string.IsNullOrWhiteSpace(input.Name)) subjectProject.Name = input.Name;
            if (!string.IsNullOrWhiteSpace(input.SubjectStyle)) subjectProject.SubjectStyle = input.SubjectStyle;
            if (!string.IsNullOrWhiteSpace(input.Type)) subjectProject.Type = input.Type;
            if (input.Credit.HasValue) subjectProject.Credit = input.Credit.Value;
            if (!string.IsNullOrWhiteSpace(input.AimedAt)) subjectProject.AimedAt = input.AimedAt;
            if (input.IsCompulsory.HasValue) subjectProject.IsCompulsory = input.IsCompulsory.Value;
            if (!string.IsNullOrWhiteSpace(input.TeachingStyle)) subjectProject.TeachingStyle = input.TeachingStyle;
            if (!string.IsNullOrWhiteSpace(input.Discription)) subjectProject.Discription = input.Discription;
            if (input.SemesterId.HasValue) subjectProject.SemesterId = input.SemesterId.Value;

            //执行修改数据方法
            await _subjectProjectRepository.UpdateAsync(subjectProject);
        }

        public async Task<SubjectProjectDto> Get(SubjectProjectQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _subjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SubjectProjectDto>();
        }

        public async Task<SubjectProjectDto> Get(int id)
        {
            var result = await _subjectProjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SubjectProjectDto>();
        }

        public async Task<PagedResultDto<SubjectProjectDto>> Query(SubjectProjectQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取总数
            var totalcount = await _subjectProjectRepository.CountAsync();

            //获取查询对象
            var query = _subjectProjectRepository.GetAll();

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                );
            }

            //添加所属学期条件
            if (input.SemesterId.HasValue)
            {
                query = query.Where(x => x.SemesterId == input.SemesterId.Value);
            }

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var subjectProjects = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<SubjectProjectDto>(totalcount, subjectProjects.MapTo<List<SubjectProjectDto>>());
        }

        public async Task<SubjectProjectSimpleDto> Simple(SubjectProjectQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _subjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SubjectProjectSimpleDto>();
        }

        public async Task<SubjectProjectSimpleDto> Simple(int id)
        {
            var result = await _subjectProjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SubjectProjectSimpleDto>();
        }
    }
}
