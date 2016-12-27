using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using CourseSelecting.Application.EducationV2.Course.Dto;
using CourseSelecting.Application.EducationV2.Semester.Dto;
using CourseSelecting.IRepositories.Education;

namespace CourseSelecting.Application.EducationV2.Semester
{
    public class SemesterV2AppService : CourseSelectingAppServiceBase, ISemesterV2AppService
    {
        private readonly ISemesterRepository _semesterRepository;

        public SemesterV2AppService(ISemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public async Task Add(SemesterInput input)
        {
            //验证传入参数
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.End.HasValue) throw new UserFriendlyException("传入End参数不正确！");
            if (string.IsNullOrEmpty(input.Name)) throw new UserFriendlyException("传入Name参数不正确！");

            //创建学期对象
            var semester = new Education.Semester
            {
                Name = input.Name,
                Start = input.Start.Value,
                End = input.End.Value
            };

            //执行插入数据方法
            await _semesterRepository.InsertAsync(semester);
        }

        public async Task Edit(SemesterInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入参数Id不正确。");

            //获取需要修改的对象
            var semester = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (semester == null) throw new UserFriendlyException("当前学期不存在！");

            //修改属性值
            if (!string.IsNullOrEmpty(input.Name)) semester.Name = input.Name;
            if (input.Start.HasValue) semester.Start = input.Start.Value;
            if (input.End.HasValue) semester.End = input.End.Value;
            
            //执行修改数据方法
            await _semesterRepository.UpdateAsync(semester);
        }

        public async Task Delete(int id)
        {
            await _semesterRepository.DeleteAsync(id);
        }

        public async Task<SemesterDto> Get(int id)
        {
            var result = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SemesterDto>();
        }

        public async Task<SemesterDto> Get(SemesterQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SemesterDto>();
        }

        public async Task<SemesterSimpleDto> Simple(int id)
        {
            var result = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SemesterSimpleDto>();
        }

        public async Task<SemesterSimpleDto> Simple(SemesterQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _semesterRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<SemesterSimpleDto>();
        }

        public async Task<PagedResultDto<SemesterDto>> Query(SemesterQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取总数
            var totalcount = await _semesterRepository.CountAsync();

            //获取查询对象
            var query = _semesterRepository.GetAll();

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<SemesterDto>(totalcount, courses.MapTo<List<SemesterDto>>());
        }
    }
}
