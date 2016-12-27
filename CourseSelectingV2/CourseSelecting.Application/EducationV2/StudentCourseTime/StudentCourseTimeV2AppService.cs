using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Application.EducationV2.StudentCourseTime.Dto;

namespace CourseSelecting.Application.EducationV2.StudentCourseTime
{
    public class StudentCourseTimeV2AppService : CourseSelectingAppServiceBase, IStudentCourseTimeV2AppService
    {
        private readonly IRepository<Education.StudentCourseTime> _studentCourseTimeRepository;
        private readonly IRepository<Education.CourseTime> _courseTimeRepository;
        private readonly IRepository<Education.StudentSubjectProject> _studentSubjectProjectRepository;
        private readonly IRepository<CourseSelecting.Users.Student, long> _studentRepository;

        public StudentCourseTimeV2AppService(
            IRepository<Education.StudentCourseTime> studentCourseTimeRepository,
            IRepository<Education.CourseTime> courseTimeRepository,
            IRepository<Education.StudentSubjectProject> studentSubjectProjectRepository,
            IRepository<CourseSelecting.Users.Student, long> studentRepository
            )
        {
            _studentCourseTimeRepository = studentCourseTimeRepository;
            _courseTimeRepository = courseTimeRepository;
            _studentSubjectProjectRepository = studentSubjectProjectRepository;
            _studentRepository = studentRepository;
        }

        public async Task Add(StudentCourseTimeInput input)
        {
            //验证传入参数
            if (!input.CourseTimeId.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.StudentId.HasValue) throw new UserFriendlyException("传入End参数不正确！");

            //验证学生预约的上课时间是否存在
            var courseTime = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.CourseTimeId);
            if (courseTime == null) throw new UserFriendlyException("该课程不存在！");

            //验证当前预约的学生是否存在
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.StudentId);
            if (student == null) throw new UserFriendlyException("该学生不存在！");

            //创建学生预约课程信息
            var studentCourseTime = new Education.StudentCourseTime
            {
                CourseTimeId = input.CourseTimeId.Value,
                StudentId = input.StudentId.Value
            };

            //随堂分数属性赋值
            if (input.ClassCredit.HasValue) studentCourseTime.ClassCredit = input.ClassCredit.Value;

            //执行插入数据方法
            await _studentCourseTimeRepository.InsertAsync(studentCourseTime);
        }

        public async Task Edit(StudentCourseTimeInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //获取需要修改的对象
            var studentCourseTime = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (studentCourseTime == null) throw new UserFriendlyException("当前的预约信息不存在！");

            //修改属性值
            if (input.CourseTimeId.HasValue)
            {
                //验证学生预约的上课时间是否存在
                var courseTime = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.CourseTimeId);
                if (courseTime == null) throw new UserFriendlyException("该课程不存在！");

                studentCourseTime.CourseTimeId = input.CourseTimeId.Value;
            }
            if (input.StudentId.HasValue)
            {
                //验证当前预约的学生是否存在
                var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.StudentId);
                if (student == null) throw new UserFriendlyException("该学生不存在！");

                studentCourseTime.StudentId = input.StudentId.Value;
            }
            if (input.ClassCredit.HasValue) studentCourseTime.ClassCredit = input.ClassCredit.Value;

            //执行修改数据方法
            await _studentCourseTimeRepository.UpdateAsync(studentCourseTime);
        }

        [UnitOfWork]
        public async Task Delete(int id)
        {
            var studentCourseTime = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (studentCourseTime == null) throw new UserFriendlyException("选课信息存在。");

            var courseTime =
                await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == studentCourseTime.CourseTimeId);
            if (courseTime == null) throw new UserFriendlyException("课程信息存在。");

            await _studentCourseTimeRepository.DeleteAsync(id);

            courseTime.Times = Math.Max(courseTime.Times - 1, 0);
            await _courseTimeRepository.UpdateAsync(courseTime);

            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StudentCourseTimeDto> Get(int id)
        {
            var result = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentCourseTimeDto>();
        }

        public async Task<StudentCourseTimeDto> Get(StudentCourseTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentCourseTimeDto>();
        }

        public async Task<StudentCourseTimeSimpleDto> Simple(int id)
        {
            var result = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentCourseTimeSimpleDto>();
        }

        public async Task<StudentCourseTimeSimpleDto> Simple(StudentCourseTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentCourseTimeSimpleDto>();
        }

        public async Task<PagedResultDto<StudentCourseTimeDto>> Query(StudentCourseTimeQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取查询对象
            var query = _studentCourseTimeRepository.GetAll().Where(
                x => !x.Student.IsDeleted
                && !x.CourseTime.TeacherCourse.IsDeleted
                && !x.CourseTime.TeacherCourse.Course.IsDeleted
                && !x.CourseTime.TeacherCourse.Course.SubjectProject.IsDeleted);

            //添加课程编号条件
            if (input.CourseId.HasValue)
            {
                query = query.Where(x => x.CourseTime.TeacherCourse.CourseId == input.CourseId.Value);
            }

            //添加所属上课时间条件
            if (input.CourseTimeId.HasValue)
            {
                query = query.Where(x => x.CourseTimeId == input.CourseTimeId);
            }

            //添加学生所选项目编号条件
            if (input.StudentSubjectProjectId.HasValue)
            {
                var studentSubjectProject = await _studentSubjectProjectRepository.FirstOrDefaultAsync(x => x.Id == input.StudentSubjectProjectId.Value);
                if (studentSubjectProject == null) throw new UserFriendlyException("学生所选项目不存在。");

                query = query.Where(x =>
                    x.StudentId == studentSubjectProject.StudentId
                    && x.CourseTime.TeacherCourse.Course.SubjectProjectId == studentSubjectProject.CourseId);
            }

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Student.Name.Contains(input.KeyWord)
                    || x.Student.UserName.Contains(input.KeyWord)
                    || x.CourseTime.TeacherCourse.Course.Name.Contains(input.KeyWord)
                    || x.CourseTime.TeacherCourse.Course.SubjectProject.Name.Contains(input.KeyWord)
                    );
            }

            //获取总数
            var totalcount = await Task.FromResult(query.Count());

            //添加分页条件
            if (input.PageSize.Value > 0)
            {
                query = query.OrderBy(x => x.CourseTime.Start)
                    .ThenBy(x => x.CreationTime)
                    .Skip(input.Start.Value).Take(input.PageSize.Value);
            }

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<StudentCourseTimeDto>(totalcount, courses.MapTo<List<StudentCourseTimeDto>>());
        }
    }
}
