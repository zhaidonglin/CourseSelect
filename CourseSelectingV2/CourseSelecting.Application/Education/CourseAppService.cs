using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using AutoMapper;
using CourseSelecting.Education.Dto;
using CourseSelecting.Users;


namespace CourseSelecting.Education
{
    [AbpAuthorize]
    public class CourseAppService : CourseSelectingAppServiceBase, ICourseAppService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<SubjectProject> _subjectRepository;
        private readonly IRepository<TeacherCourse> _teachercourseRepository;
        private readonly IRepository<CourseTime> _courseTimeRepository;
        private readonly IRepository<ExamTime> _courseExamTimeRepository;
        private readonly IRepository<StudentSubjectProject> _studentSubjectTimeRepository;
        private readonly IRepository<StudentCourseTime> _studentCourseTimeRepository;
        private readonly IRepository<StudentExamTime> _studentCourseExamTimeRepository;
        private readonly IRepository<Student, long> _studentRepository;

        public CourseAppService(
            IRepository<Course> courseRepository,
            IRepository<SubjectProject> subjectRepository,
            IRepository<TeacherCourse> teachercourseRepository,
            IRepository<CourseTime> courseTimeRepository,
            IRepository<ExamTime> courseExamTimeRepository,
            IRepository<StudentCourseTime> studentCourseTimeRepository,
            IRepository<StudentExamTime> studentCourseExamTimeRepository,
            IRepository<StudentSubjectProject> studentSubjectTimeRepository,
            IRepository<Student, long> studentRepository)
        {
            _studentCourseExamTimeRepository = studentCourseExamTimeRepository;
            _courseExamTimeRepository = courseExamTimeRepository;
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _teachercourseRepository = teachercourseRepository;
            _courseTimeRepository = courseTimeRepository;
            _studentSubjectTimeRepository = studentSubjectTimeRepository;
            _studentCourseTimeRepository = studentCourseTimeRepository;
            _studentRepository = studentRepository;
        }

        public async Task AddCourse(AddCourseInput input)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == input.SubjectProjectId);
           
            if (subject == null) throw new UserFriendlyException("项目不存在！");

            var course = new Course
            {
                SubjectProjectId = input.SubjectProjectId,
                Name = input.CourseName,
                Credit = input.CourseCredit,
               
                LimitNumbers = input.CourseLimitNumbers,
                Remark = input.Remark
            };
            await _courseRepository.InsertAsync(course);
        }

        //public async Task<ListResultOutput<CourseListDto>> GetCourses(GetCoursesInput input)
        public async Task<PagedResultOutput<CourseListDto>> GetCourses(GetSSCourseTimeInput input)  //1
        {
            var totalcount = await _courseRepository.CountAsync(x => !x.SubjectProject.IsDeleted);   //1
            var query = _courseRepository.GetAll().Where(x => !x.SubjectProject.IsDeleted);
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.SubjectProject.Name.Contains(input.KeyWord)
                    || x.Name.Contains(input.KeyWord)
                    /*|| x.SubjectProjectId.Contains(input.KeyWord)*/);
            }
            //if (!string.IsNullOrEmpty(input.KeyWord))
            //{
            //    query = query.Where(x =>

            //        x.Name.Contains(input.KeyWord));

            //}

            if (input.SubjectProjectId.HasValue)
            {
                query = query.Where(x => x.SubjectProjectId == input.SubjectProjectId.Value);    //9
            }

            query = query.OrderBy(x => x.CreationTime)
                //.Take(input.PageSize).Skip(input.Start);
                .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());

            //return new ListResultOutput<CourseListDto>(
            //    courses.MapTo<List<CourseListDto>>()
            return new PagedResultOutput<CourseListDto>(totalcount,      //1
                courses.MapTo<List<CourseListDto>>()                     //1
                );
        }

        public async Task<PagedResultOutput<StudentSubjectProjectDto>> GetCredits(GetCreditsInput input)  //1
        {
            var totalcount = await _studentSubjectTimeRepository.CountAsync(x => !x.SubjectProject.IsDeleted);   //1


            var query = _studentSubjectTimeRepository.GetAll().Where(x => !x.SubjectProject.IsDeleted);

            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.SubjectProject.Name.Contains(input.KeyWord)
                    || x.Student.Name.Contains(input.KeyWord)
                    /*|| x.SubjectProjectId.Contains(input.KeyWord)*/);
            }

            query = query.OrderBy(x => x.StudentId)
                .ThenBy(x => x.CreationTime)
                .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());


            return new PagedResultOutput<StudentSubjectProjectDto>(totalcount,      
                courses.MapTo<List<StudentSubjectProjectDto>>()                     
                );
        }

        public async Task<PagedResultOutput<StudentSubjectProjectDto>> GetSelfCredits(GetCreditsInput input)
        {
            var totalcount = await _studentSubjectTimeRepository.CountAsync(x => !x.SubjectProject.IsDeleted && x.StudentId == (AbpSession.UserId ?? 0) );   //1
            var query = _studentSubjectTimeRepository.GetAll().Where(x => !x.SubjectProject.IsDeleted);

            query = query.Where(x => x.StudentId == (AbpSession.UserId ?? 0));

            query = query.OrderBy(x => x.StudentId)
                .ThenBy(x => x.CreationTime)

                .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());


            return new PagedResultOutput<StudentSubjectProjectDto>(totalcount,      //1
                courses.MapTo<List<StudentSubjectProjectDto>>()                     //1
                );
        }

        public async Task<CreditDetailsDto> GetCredit(int id)
        {
            return (await _studentSubjectTimeRepository.FirstOrDefaultAsync(x => x.Id == id)).MapTo<CreditDetailsDto>();
        }


        public async Task DeleteCourse(int id)
        {
            var count = await _studentCourseTimeRepository
                .CountAsync(x => !x.CourseTime.TeacherCourse.IsDeleted
                   && x.CourseTime.TeacherCourse.CourseId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约该课程。");

            await _courseRepository.DeleteAsync(id);
        }

        //public async Task DeleteSSCourse(int id)
        //{
        //    //var count = await _studentCourseTimeRepository
        //    //    .CountAsync(x => !x.CourseTime.TeacherCourse.IsDeleted
        //    //       && x.CourseTime.TeacherCourse.CourseId == id);
        //    //if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约该课程。");

        //    await _courseRepository.DeleteAsync(id);
        //}

        public async Task EditCourse(EditCourseInput input)
        {
            var course = await _courseRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (course == null) throw new UserFriendlyException($"课程{input.Id}不存在！");

            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == input.SubjectProjectId);
            if (subject == null) throw new UserFriendlyException($"所选项目不存在！");

            course.SubjectProjectId = subject.Id;
            course.Name = input.CourseName;
            course.Credit = input.CourseCredit;
            course.LimitNumbers = input.CourseLimitNumbers;
            course.Remark = input.Remark;

            await _courseRepository.UpdateAsync(course);
        }



        public async Task EditCredits(EditCreditsInput input)
        {
            var course = await _studentSubjectTimeRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            
            course.ClassCredit = input.ClassCredit;
            course.ExamCredit = input.ExamCredit;
            course.TotalCadit = input.SubjectCredit;

            await _studentSubjectTimeRepository.UpdateAsync(course);


        }



        public async Task<CourseDetailsDto> GetCourse(int id)
        {
            return (await _courseRepository.FirstOrDefaultAsync(x => x.Id == id)).MapTo<CourseDetailsDto>();
        }

        public async Task<ListResultOutput<TeacherCourseDto>> GetTeacherCourses(long teacherid)
        {
            var query = _teachercourseRepository.GetAll();
            query = query.Where(x => x.TeacherId == teacherid)
                .Where(x => !x.Course.IsDeleted)
                .Where(x => !x.Course.SubjectProject.IsDeleted);

            var teacherCourses = await Task.FromResult(query.ToList());

            return new ListResultOutput<TeacherCourseDto>(teacherCourses.MapTo<List<TeacherCourseDto>>());
        }


        [UnitOfWork]
        public async Task EditTeacherCourses(EditTeacherCoursesInput input)
        {
            //查询所有教师已存在的课程
            var teacherCourses = await _teachercourseRepository.GetAllListAsync(x => x.TeacherId == input.Id);
            //根据编辑后的教师课程和已存在的教师课程，查出需要删除的教师课程
            var deletedTeacherCoursesIds =
                teacherCourses.Where(teacherCourse => !input.CoursesIds.Contains(teacherCourse.CourseId))
                    .Select(x => x.Id).ToList();

            //确认需要删除的教师课程没有学生预约
            var count =
                await _studentCourseTimeRepository.CountAsync(
                    x => deletedTeacherCoursesIds.Contains(x.CourseTime.TeacherCourseId));
            if (count > 0) throw new UserFriendlyException("编辑失败，已有学生预约了被删除的课程。");

            //删除需要删除的教师课程
            await _teachercourseRepository.DeleteAsync(x => deletedTeacherCoursesIds.Contains(x.Id));

            //添加需要添加的教师课程
            foreach (var courseid in input.CoursesIds.Where(courseid => !teacherCourses.Select(x => x.CourseId).Contains(courseid)))
            {
                await _teachercourseRepository.InsertAsync(new TeacherCourse
                {
                    CourseId = courseid,
                    TeacherId = input.Id
                });
            }

            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<CourseTimeDto> CourseTimeAdd(CourseTimeAddInput input)
        {
            var selectDate = input.Date.AddDays(1);

            var start = new DateTime(selectDate.Year, selectDate.Month, selectDate.Day, input.Start.Hour, input.Start.Minute, 0);

            var end = new DateTime(input.End.Year, input.End.Month, input.End.Day);

            var teacherCourse = await _teachercourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("当前教师未选择教授该课程。");
            
            var courseTime = new CourseTime
            {
                TeacherCourseId = input.TeacherCourseId,
                TeacherCourse = teacherCourse,
                Start = start,
                End = end,
                FitGrade = input.EnabledGrade,
                Address = input.Address
            };

            courseTime.Id = _courseTimeRepository.InsertAndGetId(courseTime);
            return courseTime.MapTo<CourseTimeDto>();
        }

        public async Task<ListResultOutput<CourseTimeDto>> GetTeacherCourseTimes(GetCourseTimeEventInput input)
        {
            input.Start = input.Start.AddDays(-1);
            input.End = input.End.AddDays(1);

            //var studentGrade = "1,";

            var query = _courseTimeRepository.GetAll();
            //query.where(enabledGrade.split.cona(“一年级”)
            query = query.Where(x => !x.TeacherCourse.IsDeleted)
                //.Where(x => x.FitGrade.Contains(studentGrade))
                .Where(x => !x.TeacherCourse.Course.IsDeleted)
                .Where(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted)
                .Where(x => x.TeacherCourse.TeacherId == (AbpSession.UserId ?? 0))
                .Where(x => x.Start > input.Start && x.Start < input.End);

            var courseTimes = await Task.FromResult(query.ToList());

            return new ListResultOutput<CourseTimeDto>(courseTimes.MapTo<List<CourseTimeDto>>());
        }

        public async Task CourseTimeDelete(int id)
        {
            var count = await _studentCourseTimeRepository.CountAsync(x => x.CourseTimeId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约了该课程。");

            await _courseTimeRepository.DeleteAsync(id);
        }

        public async Task<ListResultOutput<StudentEnabledCourseTimeDto>> GetStudentCourseTimes(GetCourseTimeEventInput input)
        {
            input.Start = input.Start.AddDays(-1);
            input.End = input.End.AddDays(1);

            var studentid = AbpSession.UserId ?? 0;
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == studentid);
            if (student == null) throw new UserFriendlyException("用户不存在。");

            var studentSubjects = await _studentSubjectTimeRepository.GetAllListAsync(x => x.StudentId == studentid);

            var studentSubjectsIds = studentSubjects.Select(i => i.CourseId).ToList();

            var studentCourses = await _courseRepository.GetAllListAsync(x => studentSubjectsIds.Contains(x.SubjectProjectId));

            var studentCoursesIds = studentCourses.Select(i => i.Id).ToList();

            var studentSelectedCourseTime = await _studentCourseTimeRepository.GetAllListAsync(
                x => x.StudentId == studentid
                && !x.CourseTime.TeacherCourse.IsDeleted
                && !x.CourseTime.TeacherCourse.Course.IsDeleted);
            var studentSelectedCourseTimeIds = studentSelectedCourseTime.Select(x => x.CourseTimeId).ToList();

            var query = _courseTimeRepository.GetAll();

            {
                var s = ((DateTime.Today.Year - student.EntryDate.Year) * 12 + ( DateTime.Today.Month - student.EntryDate.Month)) / 12 + 1;


                var studentGrade = $",{s},";
                query = query.Where(x => x.FitGrade == null || x.FitGrade == "" || x.FitGrade.Contains(studentGrade));
            }


            //Query = Query.Where(x => );
            query = query.Where(x => studentCoursesIds.Contains(x.TeacherCourse.CourseId) && x.Start > input.Start && x.End < input.End);

            var studentEnabledCourseTimes = await Task.FromResult(query.Select(x => new
            {
                x.Id,
                x.TeacherCourse,
                x.Start,
                x.End,
                x.Address,
                x.Times,
                EnabledSelecting = x.Times < x.TeacherCourse.Course.LimitNumbers,
                EnabledEndTime = x.End >= DateTime.Today,
                EnabledStartTime = x.Start <= DateTime.Now,
                IsSelected = studentSelectedCourseTimeIds.Contains(x.Id)
            }).ToList());

            return new ListResultOutput<StudentEnabledCourseTimeDto>
                (Mapper.DynamicMap<List<StudentEnabledCourseTimeDto>>(studentEnabledCourseTimes));
        }

        [UnitOfWork]
        public async Task StudentCourseTimeOrdered(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var studentCourseTime = await _studentCourseTimeRepository.FirstOrDefaultAsync(x => x.CourseTimeId == id && x.StudentId == student.Id);
            if (studentCourseTime != null) throw new UserFriendlyException("您已预约了该课程！");

            var courseTime = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (courseTime == null) throw new UserFriendlyException("该课程不存在！");

            if (courseTime.Times >= courseTime.TeacherCourse.Course.LimitNumbers) throw new UserFriendlyException("超出限选人数！");
            if (DateTime.Today > courseTime.End.Date) throw new UserFriendlyException("该课程已截止预约！");

            if (courseTime.Start < DateTime.Now) throw new UserFriendlyException("该课程已过期！");


            studentCourseTime = new StudentCourseTime
            {
                CourseTimeId = courseTime.Id,
                StudentId = student.Id
            };

            await _studentCourseTimeRepository.InsertAsync(studentCourseTime);

            courseTime.Times++;

            await _courseTimeRepository.UpdateAsync(courseTime);

            UnitOfWorkManager.Current.SaveChanges();
        }

        [UnitOfWork]
        public async Task StudentCourseTimeDeleteOrdered(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");
            
            var courseTime = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (courseTime == null) throw new UserFriendlyException("该课程不存在！");


            await _studentCourseTimeRepository.DeleteAsync(x => x.CourseTimeId == courseTime.Id && x.StudentId == student.Id);

            courseTime.Times--;
            if (courseTime.Times < 0) courseTime.Times = 0;

            await _courseTimeRepository.UpdateAsync(courseTime);

            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<CheckTeacherEnabledDeleteOutput> CheckTeacherEnabledDeleted(long teacherid)
        {
            var count = await _studentCourseTimeRepository
                .CountAsync(x => !x.CourseTime.TeacherCourse.IsDeleted
                   && x.CourseTime.TeacherCourse.TeacherId == teacherid);

            return new CheckTeacherEnabledDeleteOutput { Enabled = count <= 0 };
        }


        public async Task<StudentCourseTimeDto> CourseExamTimeAdd(CourseTimeAddInput input)
        {
            var selectDate = input.Date.AddDays(1);

            var start = new DateTime(selectDate.Year, selectDate.Month, selectDate.Day, input.Start.Hour, input.Start.Minute, 0);

            var teacherCourse = await _teachercourseRepository.FirstOrDefaultAsync(x => x.Id == input.TeacherCourseId);
            if (teacherCourse == null) throw new UserFriendlyException("当前教师未选择教授该课程。");

            var courseTime = new ExamTime
            {
                TeacherCourseId = input.TeacherCourseId,
                TeacherCourse = teacherCourse,
                Start = start,
                Address = input.Address
            };

            return (await _courseExamTimeRepository.InsertAsync(courseTime)).MapTo<StudentCourseTimeDto>();
        }

        [UnitOfWork]
        public async Task StudentCourseExamTimeAdd(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var studentCourseTime = await _studentCourseExamTimeRepository.FirstOrDefaultAsync(x => x.ExamTimeId == id && x.StudentId == student.Id);
            if (studentCourseTime != null) throw new UserFriendlyException("您已预约了该课程考试！");

            var examTime = await _courseExamTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (examTime == null) throw new UserFriendlyException("该课程不存在！");

            //if(examTime.Times >= examTime.TeacherCourse.Course.LimitNumbers) throw new UserFriendlyException("超出限选人数！");
            if (DateTime.Today > examTime.End.Date) throw new UserFriendlyException("该课程考试已截止预约！");
            if (examTime.Start < DateTime.Now) throw new UserFriendlyException("该课程考试已过期！");
            var studentExamTime = new StudentExamTime
            {
                ExamTimeId = examTime.Id,
                StudentId = student.Id
            };

            await _studentCourseExamTimeRepository.InsertAsync(studentExamTime);

            examTime.Times++;

            await _courseExamTimeRepository.UpdateAsync(examTime);

            UnitOfWorkManager.Current.SaveChanges();
        }


        public async Task<ListResultOutput<StudentCourseTimeDto>> GetTeacherCourseExamTimes(GetExamTimeEventInput input)
        {
            input.Start = input.Start.AddDays(-1);
            input.End = input.End.AddDays(1);

            var query = _courseExamTimeRepository.GetAll();

            query = query.Where(x => !x.TeacherCourse.IsDeleted)
                .Where(x => !x.TeacherCourse.Course.IsDeleted)
                .Where(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted)
                .Where(x => x.TeacherCourse.TeacherId == (AbpSession.UserId ?? 0))
                .Where(x => x.Start > input.Start && x.Start < input.End);

            var courseTimes = await Task.FromResult(query.ToList());

            return new ListResultOutput<StudentCourseTimeDto>(courseTimes.MapTo<List<StudentCourseTimeDto>>());
        }

        public async Task<ListResultOutput<StudentEnabledExamTimeDto>> GetStudentCourseExamTimes(GetExamTimeEventInput input)
        {
            input.Start = input.Start.AddDays(-1);
            input.End = input.End.AddDays(1);

            var studentid = AbpSession.UserId ?? 0;
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == studentid);
            if (student == null) throw new UserFriendlyException("用户不存在。");

            var studentSubjects = await _studentSubjectTimeRepository.GetAllListAsync(x => x.StudentId == studentid);

            var studentSubjectsIds = studentSubjects.Select(i => i.CourseId).ToList();

            var studentCourses = await _courseRepository.GetAllListAsync(x => studentSubjectsIds.Contains(x.SubjectProjectId));

            var studentCoursesIds = studentCourses.Select(i => i.Id).ToList();

            var studentSelectedExamTime = await _studentCourseExamTimeRepository.GetAllListAsync(
                x => x.StudentId == studentid
                && !x.ExamTime.TeacherCourse.IsDeleted
                && !x.ExamTime.TeacherCourse.Course.IsDeleted);
            var studentSelectedExamTimeIds = studentSelectedExamTime.Select(x => x.ExamTimeId).ToList();

            var query = _courseExamTimeRepository.GetAll();

            var s = ((DateTime.Today.Year - student.EntryDate.Year) * 12 + (DateTime.Today.Month - student.EntryDate.Month)) / 12 + 1;


            var studentGrade = $",{s},";
            query = query.Where(x => x.FitGrade == null || x.FitGrade == "" || x.FitGrade.Contains(studentGrade));

            query = query.Where(x => studentCoursesIds.Contains(x.TeacherCourse.CourseId) && x.Start > input.Start && x.Start < input.End);

            var studentEnabledExamTimes = await Task.FromResult(query.Select(x => new
            {
                x.Id,
                x.TeacherCourse,
                x.Start,
                x.Address,
                x.Times,
                x.End,
                //EnabledSelecting = x.Times < x.TeacherCourse.Course.LimitNumbers,
                EnabledEndTime = x.End >= DateTime.Today,
                EnabledStartTime = x.Start <= DateTime.Now,
                IsSelected = studentSelectedExamTimeIds.Contains(x.Id)
            }).ToList());

            var result = new ListResultOutput<StudentEnabledExamTimeDto>
                (Mapper.DynamicMap<List<StudentEnabledExamTimeDto>>(studentEnabledExamTimes));
            return result;
        }


        public async Task CourseExamTimeDelete(int id)
        {
            var count = await _studentCourseExamTimeRepository.CountAsync(x => x.ExamTimeId == id);
            if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约了该课程。");

            await _courseExamTimeRepository.DeleteAsync(id);
        }

        [UnitOfWork]
        public async Task StudentCourseExamTimeDeleteOrdered(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var examTime= await _courseExamTimeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (examTime == null) throw new UserFriendlyException("该课程不存在！");

            await _studentCourseExamTimeRepository.DeleteAsync(x => x.ExamTimeId == id && x.StudentId == student.Id);

            examTime.Times--;
            if (examTime.Times < 0) examTime.Times = 0;

            await _courseExamTimeRepository.UpdateAsync(examTime);

            UnitOfWorkManager.Current.SaveChanges();
        }


       // public async Task<PagedResultOutput<SSCourseTimeDto>> GetSSCourseTimeTables(GetCourseTimeInput input)   //8
        public async Task<PagedResultOutput<SSCourseTimeDto>> GetSSCourseTimeTables(GetSSCourseTimeInput input)
        {
            var totalcount = await _studentCourseTimeRepository.CountAsync(x => !x.CourseTime.TeacherCourse.Course.SubjectProject.IsDeleted);
            var query = _studentCourseTimeRepository.GetAll().Where(x => !x.CourseTime.TeacherCourse.Course.SubjectProject.IsDeleted);
            //if (!string.IsNullOrEmpty(input.KeyWord))
            //{
            //    query = query.Where(x =>
            //        x.SubjectProject.Name.Contains(input.KeyWord)
            //        || x.Name.Contains(input.KeyWord)
            //        );
            //}


            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.CourseTime.Address.Contains(input.KeyWord)
                    || x.CourseTime.TeacherCourse.Course.Name.Contains(input.KeyWord)
                     ||x.CourseTime.TeacherCourse.Teacher.Surname.Contains(input.KeyWord)
                    //|| x.Start.Contains(input.KeyWord)
                    );
            }


            //if (input.SubjectProjectId.HasValue)
            //{
            //    query = query.Where(x => x.SubjectProjectId == input.SubjectProjectId.Value);
            //}

            query = query.Where(x=>x.StudentId == input.StudentId)
                .OrderBy(x => x.CreationTime)
       
                .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());

       
            return new PagedResultOutput<SSCourseTimeDto>(totalcount,      //1
                courses.MapTo<List<SSCourseTimeDto>>()                     //1
                );
        }

        public async Task DeleteSSCourse(int id)
        {
            //var count = await _studentCourseTimeRepository
            //    .CountAsync(x => !x.CourseTime.TeacherCourse.IsDeleted
            //       && x.CourseTime.TeacherCourse.CourseId == id);
            //if (count > 0) throw new UserFriendlyException("删除失败，已有学生预约该课程。");

            await _studentCourseTimeRepository.DeleteAsync(id);
        }

    }
}
