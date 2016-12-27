using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.Education;
using CourseSelecting.Users.Dto;
using Microsoft.AspNet.Identity;
using CourseSelecting.Education.Dto;    //8
using System;

namespace CourseSelecting.Users
{
    [AbpAuthorize]
    public class TeacherAppService : CourseSelectingAppServiceBase, ITeacherAppService
    {
        private readonly IRepository<Teacher, long> _teacherRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<TeacherCourse> _teacherCourseRepository;
        private readonly IRepository<CourseTime> _courseTimeRepository;
        private readonly IRepository<Student, long> _studentRepository;            //10
        private readonly object id;


        public TeacherAppService(
            IRepository<Teacher, long> teacherRepository,
             IRepository<Student, long> studentRepository,      //10
            IRepository<User, long> userRepository,
            RoleManager roleManage,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<TeacherCourse> teacherCourseRepository,
            IRepository<CourseTime> courseTimeRepository)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;             //10
            _userRepository = userRepository;
            _roleManager = roleManage;
            _userRoleRepository = userRoleRepository;
            _teacherCourseRepository = teacherCourseRepository;
            _courseTimeRepository = courseTimeRepository;
        }

        public async Task<PagedResultOutput<TeacherListDto>> GetTeachers(GetTeachersInput input)
        {
            var totalcount = await _teacherRepository.CountAsync(x => !x.IsDeleted);

            var query = _teacherRepository.GetAll();

            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Surname.Contains(input.KeyWord)
                    || x.UserName.Contains(input.KeyWord)
                    || x.TeacherNo.Contains(input.KeyWord));
            }

            query = query.OrderBy(x => x.CreationTime)
            .Skip(input.Start).Take(input.PageSize);

            var teachers = await Task.FromResult(query.ToList());

            return new PagedResultOutput<TeacherListDto>(totalcount,
                teachers.MapTo<List<TeacherListDto>>()
                );
        }


        public async Task CreatTeacherInfo(EditTeacherInput input)
        {
            var teacher = _teacherRepository.FirstOrDefault(x => x.Id == input.Id);
            if (teacher == null) throw new UserFriendlyException($"登陆账号{input.LoginId}不存在！");
            //if (teacher == null)
            //{
            //    var existsTeacher = _teacherRepository.FirstOrDefault(x => x.UserName == input.LoginId.ToString());
            //    if (existsTeacher != null) throw new UserFriendlyException($"登陆账号{input.LoginId}已存在！");
            //}
            //TenantId = AbpSession.TenantId,
            //    Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword),
            //    UserName = "mingzi",
            //    Name = "mingzi",
            //    Surname = "StudentName",
            //    EmailAddress = "53590040@qq.com",



            teacher.Name = input.TeacherName;
            teacher.TeacherNo = input.TeacherNo;

            teacher.Department = input.Department;



            if (input.Gender.HasValue)
            {
                teacher.Gender = input.Gender.Value;
            }
            teacher.Major = input.Major;
            teacher.Diploma = input.Diploma;
            teacher.Degree = input.Degree;
            teacher.PositionalTitle = input.PositionalTitle;
            teacher.YearsOfWorking = input.YearsOfWorking;
            teacher.YearsOfTeaching = input.YearsOfTeaching;
            //teacher.IsFullTime = input.IsFullTime;

            teacher.Tel = input.Tel;           //1111

            if (input.IsFullTime.HasValue)
            {
                teacher.IsFullTime = input.IsFullTime.Value;
            }

            teacher.IsEmailConfirmed = true;

            teacher = await _teacherRepository.UpdateAsync(teacher);
        }


        public async Task AddTeacher(AddTeacherInput input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.LoginId);
            if (user != null) throw new UserFriendlyException($"登陆账号{input.LoginId}已存在！");

            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.CourseSelecting.Teacher);
            if (role == null) throw new UserFriendlyException("系统未初始化！");

            var teacher = new Teacher
            {
                TenantId = AbpSession.TenantId,
                Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword),
                UserName = input.LoginId,
                Name = input.TeacherName,
                Surname = input.TeacherName,
                EmailAddress = "53590040@qq.com",
                Department = input.Department,
                TeacherNo = input.TeacherNo,

                Gender = input.Gender ?? 0,
                Major = input.Major,
                Diploma = input.Diploma,
                Degree = input.Degree,
                PositionalTitle = input.PositionalTitle,
                YearsOfWorking = input.YearsOfWorking,
                YearsOfTeaching = input.YearsOfTeaching,
                IsFullTime = input.IsFullTime ?? true,
                Tel = input.Tel
            };

            teacher = await _teacherRepository.InsertAsync(teacher);

            var userRole = new UserRole
            {
                TenantId = AbpSession.TenantId,
                RoleId = role.Id,
                UserId = teacher.Id
            };

            await _userRoleRepository.InsertAsync(userRole);
        }

        public async Task EditTeacher(EditTeacherInput input)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (teacher == null) throw new UserFriendlyException($"登陆账号{input.LoginId}不存在！");

            if (input.LoginId != teacher.UserName)
            {
                var existsUser = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.LoginId);
                if (existsUser != null) throw new UserFriendlyException($"登陆账号{input.LoginId}已存在！");
            }

            teacher.Name = input.TeacherName;
            teacher.Surname = input.TeacherName;
            teacher.UserName = input.LoginId;
            teacher.Department = input.Department;
            teacher.TeacherNo = input.TeacherNo;

            teacher.Gender = input.Gender ?? 0;
            teacher.Major = input.Major;
            teacher.Diploma = input.Diploma;
            teacher.Degree = input.Degree;
            teacher.PositionalTitle = input.PositionalTitle;
            teacher.YearsOfWorking = input.YearsOfWorking;
            teacher.YearsOfTeaching = input.YearsOfTeaching;
            teacher.IsFullTime = input.IsFullTime ?? true;
            teacher.Tel = input.Tel;

            await _teacherRepository.UpdateAsync(teacher);
        }

        public TeacherDetailsDto GetTeacher(long id)
        {
            return _teacherRepository.FirstOrDefault(x => x.Id == id).MapTo<TeacherDetailsDto>();
        }

        [UnitOfWork]
        public async Task DeleteTeacher(long id)
        {
            await _courseTimeRepository.DeleteAsync(x => x.TeacherCourse.TeacherId == id);
            await _teacherCourseRepository.DeleteAsync(x => x.TeacherId == id);
            await _teacherRepository.DeleteAsync(id);

            UnitOfWorkManager.Current.SaveChanges();
        }
        public async Task ResetTeacher(long id)
        {
            //await _courseTimeRepository.ResetAsync(x => x.TeacherCourse.TeacherId == id);
            //await _teacherCourseRepository.ResetAsync(x => x.TeacherId == id);
            var teacher = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null) throw new UserFriendlyException("当前教师不存在，请重新登录。");

            teacher.Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword);

            await _teacherRepository.UpdateAsync(teacher);

        }

        public async Task<PagedResultOutput<CourseTimeDto>> GetTeacherCourseTables(GetSSCourseTimeInput input)
        {
            var totalcount = await _courseTimeRepository.CountAsync(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);
            var query = _courseTimeRepository.GetAll().Where(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);



            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Address.Contains(input.KeyWord)
                    || x.TeacherCourse.Course.Name.Contains(input.KeyWord)
                   
                    );
            }




            query = query.Where(x => x.TeacherCourse.TeacherId == input.TeacherId)
                .OrderBy(x => x.CreationTime)

                .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());


            return new PagedResultOutput<CourseTimeDto>(totalcount,      //1
                courses.MapTo<List<CourseTimeDto>>()                     //1
                );
        }


        public async Task DeleteTeacherCourseTables(int id)
        {
       

            await _courseTimeRepository.DeleteAsync(id);
        }

        public Task DeleteStudentCourseTables(int id)
        {
            throw new NotImplementedException();
        }
        //public async Task<CourseTimeDto> GetCourseTime(int id)
        //{
        //    var a = await _courseTimeRepository.FirstOrDefaultAsync(x => x.Id == id);

        //    return a.MapTo<CourseTimeDto>();
        //throw new NotImplementedException();
        //var totalcount = await _courseTimeRepository.CountAsync(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);
        //var query = _courseTimeRepository.GetAll().Where(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);
        //query = query.Where(x => x.TeacherCourse.TeacherId == id)
        //   .OrderBy(x => x.CreationTime);
        /* .Skip(input.Start).Take(input.PageSize);  */   //1

        //var courses = await Task.FromResult(query.ToList());


        //return new PagedResultOutput<CourseTimeDto>(totalcount,      //1
        //    courses.MapTo<List<CourseTimeDto>>()                     //1
        //);
        //return _courseTimeRepository.FirstOrDefault(x => x.Id == id).MapTo<CourseTimeDto>();


        //}
        public async Task<PagedResultOutput<CourseTimeDto>> GetCourseTime(GetSSCourseTimeInput input)
        {
            
            var totalcount = await _courseTimeRepository.CountAsync(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);
            var query = _courseTimeRepository.GetAll().Where(x => !x.TeacherCourse.Course.SubjectProject.IsDeleted);
            query = query.Where(x => x.TeacherCourse.TeacherId == input.TeacherId)
               .OrderBy(x => x.CreationTime)

               .Skip(input.Start).Take(input.PageSize);     //1

            var courses = await Task.FromResult(query.ToList());


            return new PagedResultOutput<CourseTimeDto>(totalcount,      //1
                courses.MapTo<List<CourseTimeDto>>()                     //1
                );
            //return _courseTimeRepository.FirstOrDefault(x => x.Id == id).MapTo<CourseTimeDto>();


        }

        //public Task<CourseTimeDto> GetCourseTime(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<CourseTimeDto> GetCourseTime(int id);  //10









    }
}


