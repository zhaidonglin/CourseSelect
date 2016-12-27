using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Application.EducationV2.StudentExamTime.Dto;

namespace CourseSelecting.Application.EducationV2.StudentExamTime
{
    public class StudentExamTimeV2AppService : CourseSelectingAppServiceBase, IStudentExamTimeV2AppService
    {
        private readonly IRepository<Education.StudentExamTime> _studentExamTimeRepository;
        private readonly IRepository<Education.ExamTime> _examTimeRepository;
        private readonly IRepository<CourseSelecting.Users.Student, long> _studentRepository;

        public StudentExamTimeV2AppService(
            IRepository<Education.StudentExamTime> studentExamTimeRepository,
            IRepository<Education.ExamTime> examTimeRepository,
            IRepository<CourseSelecting.Users.Student, long> studentRepository
            )
        {
            _studentExamTimeRepository = studentExamTimeRepository;
            _examTimeRepository = examTimeRepository;
            _studentRepository = studentRepository;
        }

        public async Task Add(StudentExamTimeInput input)
        {
            //验证传入参数
            if (!input.ExamTimeId.HasValue) throw new UserFriendlyException("传入Start参数不正确！");
            if (!input.StudentId.HasValue) throw new UserFriendlyException("传入End参数不正确！");

            //验证学生预约的考试时间是否存在
            var examTime = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == input.ExamTimeId);
            if (examTime == null) throw new UserFriendlyException("该课程不存在！");

            //验证当前预约的学生是否存在
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.StudentId);
            if (student == null) throw new UserFriendlyException("该学生不存在！");

            //创建学生预约考试信息
            var studentExamTime = new Education.StudentExamTime
            {
                ExamTimeId = input.ExamTimeId.Value,
                StudentId = input.StudentId.Value
            };

            //随堂分数属性赋值
            if (input.Credit.HasValue) studentExamTime.Credit = input.Credit.Value;

            //执行插入数据方法
            await _studentExamTimeRepository.InsertAsync(studentExamTime);
        }

        public async Task Edit(StudentExamTimeInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //获取需要修改的对象
            var studentExamTime = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (studentExamTime == null) throw new UserFriendlyException("当前的预约信息不存在！");

            //修改属性值
            if (input.ExamTimeId.HasValue)
            {
                //验证学生预约的上课时间是否存在
                var courseTime = await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == input.ExamTimeId);
                if (courseTime == null) throw new UserFriendlyException("该课程不存在！");

                studentExamTime.ExamTimeId = input.ExamTimeId.Value;
            }
            if (input.StudentId.HasValue)
            {
                //验证当前预约的学生是否存在
                var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.StudentId);
                if (student == null) throw new UserFriendlyException("该学生不存在！");

                studentExamTime.StudentId = input.StudentId.Value;
            }
            if (input.Credit.HasValue) studentExamTime.Credit = input.Credit.Value;

            //执行插入数据方法
            await _studentExamTimeRepository.UpdateAsync(studentExamTime);
        }

        [UnitOfWork]
        public async Task Delete(int id)
        {
            var studentCourseTime = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (studentCourseTime == null) throw new UserFriendlyException("选课信息存在。");

          
            var courseTime =
                await _examTimeRepository.FirstOrDefaultAsync(x => x.Id == studentCourseTime.ExamTimeId);
            if (courseTime == null) throw new UserFriendlyException("课程信息存在。");

            await _studentExamTimeRepository.DeleteAsync(id);

            courseTime.Times = Math.Max(courseTime.Times - 1, 0);
            await _examTimeRepository.UpdateAsync(courseTime);

            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StudentExamTimeDto> Get(int id)
        {
            var result = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentExamTimeDto>();
        }

        public async Task<StudentExamTimeDto> Get(StudentExamTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentExamTimeDto>();
        }

        public async Task<StudentExamTimeSimpleDto> Simple(int id)
        {
            var result = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentExamTimeSimpleDto>();
        }

        public async Task<StudentExamTimeSimpleDto> Simple(StudentExamTimeQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentExamTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentExamTimeSimpleDto>();
        }

        public async Task<PagedResultDto<StudentExamTimeDto>> Query(StudentExamTimeQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取查询对象
            var query = _studentExamTimeRepository.GetAll();

            //添加所属上课时间条件
            if (input.ExamTimeId.HasValue)
            {
                query = query.Where(x => x.ExamTimeId == input.ExamTimeId);
            }

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Student.Name.Contains(input.KeyWord)
                    || x.Student.UserName.Contains(input.KeyWord)
                    || x.ExamTime.TeacherCourse.Course.Name.Contains(input.KeyWord)
                    || x.ExamTime.TeacherCourse.Course.SubjectProject.Name.Contains(input.KeyWord)
                    || x.ExamTime.TeacherCourse.Teacher.Name.Contains(input.KeyWord)
                    || x.ExamTime.Address.Contains(input.KeyWord)
                );
            }

            //获取总数
            var totalcount = await Task.FromResult(query.Count());

            if (input.PageSize.Value > 0)
            {
                //添加分页条件
                query = query.OrderBy(x => x.ExamTime.Start)
                    .ThenBy(x => x.CreationTime)
                    .Skip(input.Start.Value).Take(input.PageSize.Value);
            }

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<StudentExamTimeDto>(totalcount, courses.MapTo<List<StudentExamTimeDto>>());
        }
    }
}
