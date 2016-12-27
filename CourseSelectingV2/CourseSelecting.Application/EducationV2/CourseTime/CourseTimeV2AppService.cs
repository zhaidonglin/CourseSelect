using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using CourseSelecting.Application.EducationV2.CourseTime.Dto;

namespace CourseSelecting.Application.EducationV2.CourseTime
{
    public class CourseTimeV2AppService : CourseSelectingAppServiceBase, ICourseTimeV2AppService
    {
        private readonly IRepository<Education.CourseTime> _courseTimeRepository;
        private readonly IRepository<Education.TeacherCourse> _teacherCourseRepository;
        private readonly IRepository<Education.StudentCourseTime> _studentCourseTimeRepository;
        public CourseTimeV2AppService(
            IRepository<Education.CourseTime> courseTimeRepository,
            IRepository<Education.TeacherCourse> teacherCourseRepository,
            IRepository<Education.StudentCourseTime> studentCourseTimeRepository
            )
        {
            _courseTimeRepository = courseTimeRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _studentCourseTimeRepository = studentCourseTimeRepository;
        }

        public async Task Add(CourseTimeInput input)
        {
            //验证传入参数
            if (!input.TeacherCourseId.HasValue) throw new UserFriendlyException("传入TeacherCourseId参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.End.HasValue) throw new UserFriendlyException("传入End参数不正确！");
            if (string.IsNullOrEmpty(input.FitGrade)) throw new UserFriendlyException("传入FitGrade参数不正确！");
            if (string.IsNullOrEmpty(input.Address)) throw new UserFriendlyException("传入Address参数不正确！");

            //验证教师课程信息是否存在
            var teacherCourse = _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("该教师没有发表该课程的权限。");

            //创建上课时间对象
            var courseTime = new Education.CourseTime
            {
                TeacherCourseId = input.TeacherCourseId.Value,
                Start = input.Start.Value,
                End = input.End.Value,
                FitGrade = input.FitGrade,
                Address = input.Address
            };

            if (input.Weeks.HasValue) courseTime.Weeks = input.Weeks.Value;
            if (input.Times.HasValue) courseTime.Times = input.Times.Value;

            //执行插入数据方法
            await _courseTimeRepository.InsertAsync(courseTime);
        }

        public async Task Edit(CourseTimeInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入参数Id不正确。");

            //获取需要修改的对象
            var courseTime = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (courseTime == null) throw new UserFriendlyException("当前上课时间不存在！");

            //验证教师课程信息是否存在
            var teacherCourse = _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("该教师没有发表该课程的权限。");

            //修改属性值
            if (input.TeacherCourseId.HasValue) courseTime.TeacherCourseId = input.TeacherCourseId.Value;
            if (input.Start.HasValue) courseTime.Start = input.Start.Value;
            if (input.End.HasValue) courseTime.End = input.End.Value;
            if (!string.IsNullOrEmpty(input.FitGrade)) courseTime.FitGrade = input.FitGrade;
            if (!string.IsNullOrEmpty(input.Address)) courseTime.Address = input.Address;
            if (input.Weeks.HasValue) courseTime.Weeks = input.Weeks.Value;
            if (input.Times.HasValue) courseTime.Times = input.Times.Value;

            //执行修改数据方法
            await _courseTimeRepository.UpdateAsync(courseTime);
        }

        public async Task Delete(int id)
        {
            var count = await _studentCourseTimeRepository.CountAsync(x => x.CourseTimeId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约了该课程。");

            await _courseTimeRepository.DeleteAsync(id);
        }

        public async Task<CourseTimeDto> Get(int id)
        {
            var result = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseTimeDto>();
        }

        public async Task<CourseTimeDto> Get(CourseTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseTimeDto>();
        }

        public async Task<CourseTimeSimpleDto> Simple(int id)
        {
            var result = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseTimeSimpleDto>();
        }

        public async Task<CourseTimeSimpleDto> Simple(CourseTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<CourseTimeSimpleDto>();
        }

        public async Task<PagedResultDto<CourseTimeDto>> Query(CourseTimeQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取查询对象
            var query = _courseTimeRepository.GetAll();

            //添加授课教师条件
            if (input.TeacherId.HasValue)
            {
                query = query.Where(x => x.TeacherCourse.TeacherId == input.TeacherId.Value);
            }

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.TeacherCourse.Course.Name.Contains(input.KeyWord)
                );
            }

            //获取总数
            var totalcount = await Task.FromResult(query.Count());

            //添加分页条件
            query = query.OrderBy(x => x.Start)
                .ThenBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<CourseTimeDto>(totalcount, courses.MapTo<List<CourseTimeDto>>());
        }
    }
}
