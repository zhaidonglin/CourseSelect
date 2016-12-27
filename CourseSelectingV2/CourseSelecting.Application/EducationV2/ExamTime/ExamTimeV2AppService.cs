using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Application.EducationV2.ExamTime.Dto;

namespace CourseSelecting.Application.EducationV2.ExamTime
{
    public class ExamTimeV2AppService : CourseSelectingAppServiceBase, IExamTimeV2AppService
    {
        private readonly IRepository<Education.ExamTime> _examTimeRepository;
        private readonly IRepository<Education.TeacherCourse> _teacherCourseRepository;
        private readonly IRepository<Education.StudentExamTime> _studentExamTimeRepository;
        public ExamTimeV2AppService(
            IRepository<Education.ExamTime> examTimeRepository,
            IRepository<Education.TeacherCourse> teacherCourseRepository,
            IRepository<Education.StudentExamTime> studentExamTimeRepository
            )
        {
            _examTimeRepository = examTimeRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _studentExamTimeRepository = studentExamTimeRepository;
        }

        public async Task Add(ExamTimeInput input)
        {
            //验证传入参数
            if (!input.TeacherCourseId.HasValue) throw new UserFriendlyException("传入TeacherCourseId参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.End.HasValue) throw new UserFriendlyException("传入End参数不正确！");
            if (string.IsNullOrEmpty(input.FitGrade)) throw new UserFriendlyException("传入FitGrade参数不正确！");
            if (string.IsNullOrEmpty(input.Address)) throw new UserFriendlyException("传入Address参数不正确！");

            //验证教师课程信息是否存在
            var teacherCourse = _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("该教师没有发表该课程考试的权限。");

            //创建上课时间对象
            var examTime = new Education.ExamTime
            {
                TeacherCourseId = input.TeacherCourseId.Value,
                Start = input.Start.Value,
                End = input.End.Value,
                FitGrade = input.FitGrade,
                Address = input.Address
            };

            if (input.Weeks.HasValue) examTime.Weeks = input.Weeks.Value;
            if (input.Times.HasValue) examTime.Times = input.Times.Value;
            if (!string.IsNullOrEmpty(input.Teacher)) examTime.Teacher = input.Teacher;

            //执行插入数据方法
            await _examTimeRepository.InsertAsync(examTime);
        }

        [UnitOfWork]
        public async Task<ExamTimeDto> AddAndGetObj(ExamTimeInput input)
        {
            //验证传入参数
            if (!input.TeacherCourseId.HasValue) throw new UserFriendlyException("传入TeacherCourseId参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.End.HasValue) throw new UserFriendlyException("传入End参数不正确！");
            if (string.IsNullOrEmpty(input.FitGrade)) throw new UserFriendlyException("传入FitGrade参数不正确！");
            if (string.IsNullOrEmpty(input.Address)) throw new UserFriendlyException("传入Address参数不正确！");

            //验证教师课程信息是否存在
            var teacherCourse = await _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("该教师没有发表该课程考试的权限。");

            //创建上课时间对象
            var examTime = new Education.ExamTime
            {
                TeacherCourseId = input.TeacherCourseId.Value,
                Start = input.Start.Value,
                End = input.End.Value,
                FitGrade = input.FitGrade,
                Address = input.Address
            };

            if (input.Weeks.HasValue) examTime.Weeks = input.Weeks.Value;
            if (input.Times.HasValue) examTime.Times = input.Times.Value;
            if (!string.IsNullOrEmpty(input.Teacher)) examTime.Teacher = input.Teacher;

            //执行插入数据方法
            var id = await _examTimeRepository.InsertAndGetIdAsync(examTime);
            UnitOfWorkManager.Current.SaveChanges();

            return (await _examTimeRepository.GetAsync(id)).MapTo<ExamTimeDto>();
        }

        public async Task Edit(ExamTimeInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入参数Id不正确。");

            //获取需要修改的对象
            var examTime = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (examTime == null) throw new UserFriendlyException("当前考试时间不存在！");
            
            //验证教师课程信息是否存在
            var teacherCourse = _teacherCourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("该教师没有发表该课程考试的权限。");

            //修改属性值
            if (input.TeacherCourseId.HasValue) examTime.TeacherCourseId = input.TeacherCourseId.Value;
            if (input.Start.HasValue) examTime.Start = input.Start.Value;
            if (input.End.HasValue) examTime.End = input.End.Value;
            if (!string.IsNullOrEmpty(input.FitGrade)) examTime.FitGrade = input.FitGrade;
            if (!string.IsNullOrEmpty(input.Address)) examTime.Address = input.Address;
            if (input.Weeks.HasValue) examTime.Weeks = input.Weeks.Value;
            if (input.Times.HasValue) examTime.Times = input.Times.Value;
            if (!string.IsNullOrEmpty(input.Teacher)) examTime.Teacher = input.Teacher;

            //执行修改数据方法
            await _examTimeRepository.UpdateAsync(examTime);
        }

        public async Task Delete(int id)
        {
            var count = await _studentExamTimeRepository.CountAsync(x => x.ExamTimeId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约了该考试。");

            await _examTimeRepository.DeleteAsync(id);
        }

        public async Task<ExamTimeDto> Get(int id)
        {
            var result = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<ExamTimeDto>();
        }

        public async Task<ExamTimeDto> Get(ExamTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<ExamTimeDto>();
        }

        public async Task<ExamTimeSimpleDto> Simple(int id)
        {
            var result = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<ExamTimeSimpleDto>();
        }

        public async Task<ExamTimeSimpleDto> Simple(ExamTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<ExamTimeSimpleDto>();
        }

        public async Task<PagedResultDto<ExamTimeDto>> Query(ExamTimeQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取查询对象
            var query = _examTimeRepository.GetAll();

            //添加上课时间满足的时间范围的起始时间参数
            if (input.BeginDateTime.HasValue)
            {
                query = query.Where(x => x.Start >= input.BeginDateTime.Value);
            }

            //添加上课时间满足的时间范围的结束时间参数
            if (input.EndDateTime.HasValue)
            {
                query = query.Where(x => x.Start <= input.EndDateTime.Value);
            }

            //添加上课时间满足的时间范围的结束时间参数
            if (input.TeacherId.HasValue)
            {
                //如果录入教师编号为-1则查询当前用户，否则根据传入teacherid查询
                if (input.TeacherId.Value == -1)
                {
                    if(!AbpSession.UserId.HasValue) throw new UserFriendlyException("用户不存在，请重新登录。");
                    query = query.Where(x => x.TeacherCourse.TeacherId == AbpSession.UserId.Value);
                }
                else
                {
                    query = query.Where(x => x.TeacherCourse.TeacherId == input.TeacherId.Value);
                }
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
            return new PagedResultOutput<ExamTimeDto>(totalcount, courses.MapTo<List<ExamTimeDto>>());
        }
    }
}
