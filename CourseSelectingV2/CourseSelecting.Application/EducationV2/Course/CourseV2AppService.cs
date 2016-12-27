using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Application.EducationV2.Course.Dto;

namespace CourseSelecting.Application.EducationV2.Course
{
    public class CourseV2AppService : CourseSelectingAppServiceBase, ICourseV2AppService
    {
        private readonly IRepository<Education.Course> _courseRepository;
        private readonly IRepository<Education.SubjectProject> _subjectRepository;
        private readonly IRepository<Education.TeacherCourse> _teacherCourseRepository;

        public CourseV2AppService(
            IRepository<Education.Course> courseRepository,
            IRepository<Education.SubjectProject> subjectRepository,
            IRepository<Education.TeacherCourse> teacherCourseRepository
            )
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _teacherCourseRepository = teacherCourseRepository;
        }

        public async Task Add(CourseInput input)
        {
            //验证传入参数
            if (!input.SubjectProjectId.HasValue) throw new UserFriendlyException("传入SubjectProjectId参数不正确！");
            if (string.IsNullOrEmpty(input.Name)) throw new UserFriendlyException("传入Name参数不正确！");
            if (!input.Credit.HasValue) throw new UserFriendlyException("传入Credit参数不正确！");
            if (!input.LimitNumbers.HasValue) throw new UserFriendlyException("传入LimitNumbers参数不正确！");
            if (!input.SelectedNumbers.HasValue) throw new UserFriendlyException("传入SelectedNumbers参数不正确！");

            //验证课程所属项目是否存在
            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == input.SubjectProjectId);
            if (subject == null) throw new UserFriendlyException($"编号为{input.SubjectProjectId}的项目不存在！");

            //创建课程对象
            var course = new Education.Course
            {
                SubjectProjectId = input.SubjectProjectId.Value,
                Name = input.Name,
                Credit = input.Credit.Value,
                LimitNumbers = input.LimitNumbers.Value,
                SelectedNumbers = input.SelectedNumbers.Value
            };

            //备注属性赋值
            if (!string.IsNullOrEmpty(input.Remark)) course.Remark = input.Remark;

            //执行插入数据方法
            await _courseRepository.InsertAsync(course);
        }

        [UnitOfWork]
        public async Task<CourseDto> AddAndGetObj(CourseInput input)
        {
            //验证传入参数
            if (!input.SubjectProjectId.HasValue) throw new UserFriendlyException("传入SubjectProjectId参数不正确！");
            if (string.IsNullOrEmpty(input.Name)) throw new UserFriendlyException("传入Name参数不正确！");
            if (!input.Credit.HasValue) throw new UserFriendlyException("传入Credit参数不正确！");
            if (!input.LimitNumbers.HasValue) throw new UserFriendlyException("传入LimitNumbers参数不正确！");
            if (!input.SelectedNumbers.HasValue) throw new UserFriendlyException("传入SelectedNumbers参数不正确！");

            //验证课程所属项目是否存在
            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == input.SubjectProjectId);
            if (subject == null) throw new UserFriendlyException($"编号为{input.SubjectProjectId}的项目不存在！");

            //创建课程对象
            var course = new Education.Course
            {
                SubjectProjectId = input.SubjectProjectId.Value,
                Name = input.Name,
                Credit = input.Credit.Value,
                LimitNumbers = input.LimitNumbers.Value,
                SelectedNumbers = input.SelectedNumbers.Value
            };

            //备注属性赋值
            if (!string.IsNullOrEmpty(input.Remark)) course.Remark = input.Remark;

            //执行插入数据方法
            var id = await _courseRepository.InsertAndGetIdAsync(course);
            UnitOfWorkManager.Current.SaveChanges();

            return (await _courseRepository.GetAsync(id)).MapTo<CourseDto>();
        }

        public async Task Edit(CourseInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //获取需要修改的对象
            var course = await _courseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (course == null) throw new UserFriendlyException("当前课程不存在！");

            //修改属性值
            if (input.SubjectProjectId.HasValue)
            {
                //验证课程所属项目是否存在
                var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == input.SubjectProjectId);
                if (subject == null) throw new UserFriendlyException($"编号为{input.SubjectProjectId}的项目不存在！");

                course.SubjectProjectId = input.SubjectProjectId.Value;
            }
            if (!string.IsNullOrWhiteSpace(input.Name)) course.Name = input.Name;
            if (input.Credit.HasValue) course.Credit = input.Credit.Value;
            if (input.LimitNumbers.HasValue) course.LimitNumbers = input.LimitNumbers.Value;
            if (input.SelectedNumbers.HasValue) course.SelectedNumbers = input.SelectedNumbers.Value;
            if (!string.IsNullOrWhiteSpace(input.Remark)) course.Remark = input.Remark;

            //执行修改数据方法
            await _courseRepository.UpdateAsync(course);
        }

        public async Task Delete(int id)
        {
            //验证教师教授课程级联关系是否存在
            var count = await _teacherCourseRepository.CountAsync(x => x.CourseId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有教师教授课程。");

            await _courseRepository.DeleteAsync(id);
        }

        public async Task<CourseDto> Get(int id)
        {
            var result = await _courseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseDto>();
        }

        public async Task<CourseDto> Get(CourseQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _courseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseDto>();
        }

        public async Task<CourseSimpleDto> Simple(int id)
        {
            var result = await _courseRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseSimpleDto>();
        }

        public async Task<CourseSimpleDto> Simple(CourseQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _courseRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseSimpleDto>();
        }

        public async Task<PagedResultDto<CourseDto>> Query(CourseQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取查询对象
            var query = _courseRepository.GetAll().Where(
                x => !x.SubjectProject.IsDeleted
                && !x.SubjectProject.Semester.IsDeleted
                );

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                );
            }

            //添加所属项目条件
            if (input.SubjectProjectId.HasValue)
            {
                query = query.Where(x => x.SubjectProjectId == input.SubjectProjectId.Value);
            }

            //获取总数
            var totalcount = await Task.FromResult(query.Count());

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<CourseDto>(totalcount, courses.MapTo<List<CourseDto>>());
        }
    }
}
