using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using CourseSelecting.Education.Dto;
using CourseSelecting.IRepositories.Education;
using CourseSelecting.Users;

namespace CourseSelecting.Education
{
    [AbpAuthorize]
    public class SubjectProjectAppService : CourseSelectingAppServiceBase, ISubjectProjectAppService
    {
        private readonly ISemesterRepository _semesterRepository;
        private readonly IRepository<SubjectProject> _subjectRepository;
        private readonly IRepository<StudentSubjectProject> _studentSubjectRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<StudentCourseTime> _studentCourseTimeRepository;
        private readonly IRepository<StudentExamTime> _studentExamTimeRepository;
        private readonly IRepository<Student, long> _studentRepository;

        public SubjectProjectAppService(ISemesterRepository semesterRepository,
            IRepository<SubjectProject> subjectRepository,
            IRepository<StudentSubjectProject> studentSubjectRepository,
            IRepository<Course> courseRepository,
            IRepository<StudentCourseTime> studentCourseTimeRepository,
            IRepository<StudentExamTime> studentExamTimeRepository,
            IRepository<Student, long> studentRepository)
        {
            _semesterRepository = semesterRepository;
            _subjectRepository = subjectRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _courseRepository = courseRepository;
            _studentCourseTimeRepository = studentCourseTimeRepository;
            _studentExamTimeRepository = studentExamTimeRepository;
            _studentRepository = studentRepository;
        }

        public async Task AddSelectSubjectProject(AddSelectSubjectProjectInput input)             //3
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            //if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var studentSubject = await _studentSubjectRepository.FirstOrDefaultAsync(
                    x => x.StudentId == student.Id && x.CourseId == input.courseId);
            if (studentSubject != null) return;

            studentSubject = new StudentSubjectProject
            {
                StudentId = student.Id,
                CourseId = input.courseId
            };

            await _studentSubjectRepository.InsertAsync(studentSubject);
        }

        public async Task AddSelectSubjectProject1(GetCourseTimeInput input)                  //3
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.studentId);
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var studentSubject = await _studentSubjectRepository.FirstOrDefaultAsync(
                    x => x.StudentId == student.Id && x.CourseId == input.courseId);
            if (studentSubject != null) return;

            studentSubject = new StudentSubjectProject
            {
                StudentId = student.Id,
                CourseId = input.courseId
            };

            await _studentSubjectRepository.InsertAsync(studentSubject);
        }

        public async Task AddSubjectProject(AddSubjectProjectInput input)
        {
            var semester = await _semesterRepository.GetCurrentSemester();
            var subject = new SubjectProject
            {
                Name = input.Name,
                SubjectStyle = input.SubjectStyle,
                Type = input.Type,
                Credit = input.Credit,
                AimedAt = input.AimedAt,
                IsCompulsory = input.IsCompulsory,
                TeachingStyle = input.TeachingStyle,
                Discription = input.Discription,
                SemesterId = semester.Id
            };

            await _subjectRepository.InsertAsync(subject);
        }

        //public async Task<ListResultOutput<SubjectProjectsListDto>> GetSubjectProjects(GetSubjectProjectsInput input)
        public async Task<PagedResultOutput<SubjectProjectsListDto>> GetSubjectProjects(GetSubjectProjectsInput input)  //1
        {
            var totalcount = await _subjectRepository.CountAsync(x => !x.IsDeleted);   //1
            var query = _subjectRepository.GetAll();
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                    || x.SubjectStyle.Contains(input.KeyWord));
            }


            query = query.OrderByDescending(x => x.IsCompulsory)
                .ThenBy(x => x.CreationTime)
                 //.Take(input.PageSize).Skip(input.Start);
                 .Skip(input.Start).Take(input.PageSize);              //1


            var subjects = await Task.FromResult(query.ToList());

            //return new ListResultOutput<SubjectProjectsListDto>(
            //    subjects.MapTo<List<SubjectProjectsListDto>>()
            return new PagedResultOutput<SubjectProjectsListDto>(totalcount,      //1
                subjects.MapTo<List<SubjectProjectsListDto>>()                     //1
                );

        }

        public async Task<ListResultOutput<SubjectProjectSimpleDto>> GetCurrentSemesterSubjectProjects()
        {
            var semester = await _semesterRepository.GetCurrentSemester();
            var query = _subjectRepository.GetAll();

            query = query.Where(x => x.SemesterId == semester.Id)
                .OrderBy(x => x.CreationTime);
            var subjects = await Task.FromResult(query.ToList());

            return new ListResultOutput<SubjectProjectSimpleDto>(
                subjects.MapTo<List<SubjectProjectSimpleDto>>()
                );
        }

        public async Task<PagedResultOutput<GetSelectSubjectProjectsListDto>> GetStudentSelectCourses(GetSubjectProjectsInput input)
        {
            var semester = await _semesterRepository.GetCurrentSemester();

            var totalcout = await _subjectRepository.CountAsync(x => x.Semester.Id == semester.Id && !x.IsDeleted);
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.studentId);

            var query = _subjectRepository.GetAll();
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                     || x.SubjectStyle.Contains(input.KeyWord));
            }


            query = query.Where(x => x.Semester.Id == semester.Id)
                .OrderByDescending(x => x.IsCompulsory)
                .ThenBy(x => x.CreationTime)
                .Skip(input.Start).Take(input.PageSize);

            var subjects = (await Task.FromResult(query.ToList())).MapTo<List<GetSelectSubjectProjectsListDto>>();

            var studentSelectedSubjects = await _studentSubjectRepository.GetAllListAsync(
                x => x.SubjectProject.Semester.Id == semester.Id
                && x.StudentId == (input.studentId));

            var ids = studentSelectedSubjects.Select(x => x.CourseId).ToList();

            foreach (var subject in subjects.Where(subject => ids.Contains(subject.Id)))
            {
                subject.IsSelected = true;
            }

            return new PagedResultOutput<GetSelectSubjectProjectsListDto>(totalcout, subjects);
        }

        public async Task<PagedResultOutput<GetSelectSubjectProjectsListDto>> GetSelectSubjectProjects(GetSubjectProjectsInput input)
        {
            var semester = await _semesterRepository.GetCurrentSemester();

            var totalcout = await _subjectRepository.CountAsync(x => x.Semester.Id == semester.Id && !x.IsDeleted);

            var query = _subjectRepository.GetAll();
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                     || x.SubjectStyle.Contains(input.KeyWord));
            }


            query = query.Where(x => x.Semester.Id == semester.Id)
                .OrderByDescending(x => x.IsCompulsory)
                .ThenBy(x => x.CreationTime)
                .Skip(input.Start).Take(input.PageSize);

            var subjects = (await Task.FromResult(query.ToList())).MapTo<List<GetSelectSubjectProjectsListDto>>();

            var studentSelectedSubjects = await _studentSubjectRepository.GetAllListAsync(
                x => x.SubjectProject.Semester.Id == semester.Id
                && x.StudentId == (AbpSession.UserId ?? 0));

            var ids = studentSelectedSubjects.Select(x => x.CourseId).ToList();

            foreach (var subject in subjects.Where(subject => ids.Contains(subject.Id)))
            {
                subject.IsSelected = true;
            }

            return new PagedResultOutput<GetSelectSubjectProjectsListDto>(totalcout, subjects);
        }
        public async Task DeleteSubjectProject(int id)
        {
            var count = await _courseRepository.CountAsync(x => !x.IsDeleted && x.SubjectProjectId == id);
            if (count > 0) throw new UserFriendlyException("该项目包含课程信息，请先删除课程！");

            await _subjectRepository.DeleteAsync(id);
        }
        public async Task selectDeleteStudentCourse(int studentId,int courseId)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == studentId);
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var coursecount = await _studentCourseTimeRepository.CountAsync(
                x => !x.CourseTime.TeacherCourse.IsDeleted
                && !x.CourseTime.TeacherCourse.Course.IsDeleted
                && x.CourseTime.TeacherCourse.Course.SubjectProjectId == courseId
                && x.StudentId == student.Id);

            if (coursecount > 0) throw new UserFriendlyException("无法取消所选项目，您已预约了该项目的课程。");

            var examcount = await _studentExamTimeRepository.CountAsync(
                x => !x.ExamTime.TeacherCourse.IsDeleted
                && !x.ExamTime.TeacherCourse.Course.IsDeleted
                && x.ExamTime.TeacherCourse.Course.SubjectProjectId == courseId
                && x.StudentId == student.Id);

            if (examcount > 0) throw new UserFriendlyException("无法取消所选项目，您已预约了该项目的考试。");


            await _studentSubjectRepository.DeleteAsync(x => x.CourseId == courseId && x.StudentId == studentId);
        }
        public async Task SelectDeleteSubjectProject(int id)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == (AbpSession.UserId ?? 0));
            if (student == null) throw new UserFriendlyException("当前用户不存在，请重新登陆。");

            var coursecount = await _studentCourseTimeRepository.CountAsync(
                x => !x.CourseTime.TeacherCourse.IsDeleted
                && !x.CourseTime.TeacherCourse.Course.IsDeleted
                && x.CourseTime.TeacherCourse.Course.SubjectProjectId == id
                && x.StudentId == student.Id);

            if(coursecount > 0) throw new UserFriendlyException("无法取消所选项目，您已预约了该项目的课程。");

            var examcount = await _studentExamTimeRepository.CountAsync(
                x => !x.ExamTime.TeacherCourse.IsDeleted
                && !x.ExamTime.TeacherCourse.Course.IsDeleted
                && x.ExamTime.TeacherCourse.Course.SubjectProjectId == id
                && x.StudentId == student.Id);

            if (examcount > 0) throw new UserFriendlyException("无法取消所选项目，您已预约了该项目的考试。");


            await _studentSubjectRepository.DeleteAsync(x => x.CourseId == id && x.StudentId == (AbpSession.UserId ?? 0));
        }
        public async Task<SubjectProjectDto> GetSubjectProject(int id)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            return subject.MapTo<SubjectProjectDto>();
        }

        public async Task EditSubjectProject(EditSubjectProjectInput input)
        {
            var subject = _subjectRepository.FirstOrDefault(x => x.Id == input.Id);
            if (subject == null) throw new UserFriendlyException($"项目不存在！");

            subject.Name = input.Name;
            subject.SubjectStyle = input.SubjectStyle;
            subject.Type = input.Type;
            subject.Credit = input.Credit;
            subject.IsCompulsory = input.IsCompulsory;
            subject.AimedAt = input.AimedAt;
            subject.TeachingStyle = input.TeachingStyle;
            subject.Discription = input.Discription;

            await _subjectRepository.UpdateAsync(subject);
        }

        public async Task<SubjectProjectCoursesDto> GetSubjectProjectCourses(int id)
        {
            var subject = await _subjectRepository.FirstOrDefaultAsync(x => x.Id == id);
            return subject.MapTo<SubjectProjectCoursesDto>();
        }

        public async Task<ListResultOutput<SubjectProjectCoursesDto>> GetSubjectProjectsWithCourses()
        {
            var semester = await _semesterRepository.GetCurrentSemester();
            var query = _subjectRepository.GetAll();



            query = query.Where(x => x.SemesterId == semester.Id)
                .OrderBy(x => x.CreationTime);
            var subjects = await Task.FromResult(query.ToList());

            return new ListResultOutput<SubjectProjectCoursesDto>(
                subjects.MapTo<List<SubjectProjectCoursesDto>>()
                );
        }

        //public Task AddSelectSubjectProject(AddSelectSubjectProjectInput input)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task AddSelectSubjectProject(AddSelectSubjectProjectInput input)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<ListResultOutput<SubjectProjectsListDto>> ISubjectProjectAppService.GetSubjectProjects(GetSubjectProjectsInput input)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
