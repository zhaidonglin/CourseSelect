//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CourseSelecting.Users.Dto;
//using Abp.Domain.Repositories;
using System;
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

namespace CourseSelecting.Users
{
    [AbpAuthorize]
    public class StudentAppService : CourseSelectingAppServiceBase, IStudentAppService
    {
        //public Task AddStudent(AddStudentInput input)
        //{
        //    var a = input;
        //    throw new NotImplementedException();

        //}
        private readonly IRepository<Student, long> _studentRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<StudentSubjectProject> _studentSubjectProjectRepository;
        private readonly IRepository<StudentCourseTime> _studentCourseTimeRepository;
        private readonly IRepository<StudentExamTime> _studentExamTimeRepository;

        public StudentAppService(
            IRepository<Student, long> studentRepository,
            IRepository<User, long> userRepository,
            RoleManager roleManage,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<StudentSubjectProject> studentSubjectProjectRepository,
            IRepository<StudentCourseTime> studentCourseTimeRepository,
            IRepository<StudentExamTime> studentExamTimeRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _roleManager = roleManage;
            _userRoleRepository = userRoleRepository;
            _studentCourseTimeRepository = studentCourseTimeRepository;
            _studentExamTimeRepository = studentExamTimeRepository;
            _studentSubjectProjectRepository = studentSubjectProjectRepository;
        }

        //public ListResultOutput<StudentListDto> GetStudents(GetStudentsInput input)
        public async Task<PagedResultOutput<StudentListDto>> GetStudents(GetStudentsInput input)  //1
        {
            var totalcount = await _studentRepository.CountAsync(x=>!x.IsDeleted);   //1
            var query = _studentRepository.GetAll();
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Surname.Contains(input.KeyWord)
                    || x.UserName.Contains(input.KeyWord)
                    || x.StudentNo.Contains(input.KeyWord));
            }



            //.OrderBy(x => x.CreationTime)
            //.Take(input.PageSize).Skip(input.Start);
            query = query.OrderBy(x => x.CreationTime)            //1
            .Skip(input.Start).Take(input.PageSize);              //1

            //var students = query.ToList();
            var students = await Task.FromResult(query.ToList());

            //return new ListResultOutput<StudentListDto>(
            //    students.MapTo<List<StudentListDto>>()
            return new PagedResultOutput<StudentListDto>(totalcount,      //1
                students.MapTo<List<StudentListDto>>()                     //1
                );
        }

        public async Task CreatStudentInfo(EditStudentInput input)
        {
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            if (student == null) throw new UserFriendlyException($"登陆账号{input.LoginId}不存在！");
            if (student.UserName != input.LoginId)
            {
                var existsUser = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.LoginId);
                if (existsUser != null) throw new UserFriendlyException($"登陆账号{input.LoginId}已存在！");
            }
            //TenantId = AbpSession.TenantId,
            //    Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword),
            //    UserName = "mingzi",
            //    Name = "mingzi",
            //    Surname = "StudentName",
            //    EmailAddress = "53590040@qq.com",



            #region 根据学号处理入学时间年级
            if (input.LoginId.Length < 4) throw new UserFriendlyException("学号格式不正确");
            var yearstr = input.LoginId.Substring(0, 4);
            int year;
            try
            {
                year = Convert.ToInt32(yearstr);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("学号格式不正确");
            }
            var entryDate = new DateTime(year, 8, 20);
            var grade = Math.Min(((DateTime.Today.Year - entryDate.Year) * 12 + (DateTime.Today.Month - entryDate.Month)) / 12, 4);
            string gradestr;
            switch (grade)
            {
                case 0:
                    gradestr = "一年级";
                    break;
                case 1:
                    gradestr = "二年级";
                    break;
                case 2:
                    gradestr = "三年级";
                    break;
                case 3:
                    gradestr = "四年级";
                    break;
                case 4:
                    gradestr = "五年级";
                    break;
                default:
                    gradestr = "五年级";
                    break;
            }
            #endregion

            //student.StudentNo = input.StudentNo;    //2
            student.StudentNo = input.LoginId;
            //student.EntryDate = input.EntryDate;
            student.Gender = input.Gender;
            student.Tel = input.Tel;
            student.Major = input.Major;
            student.Grade = gradestr;
            student.Class = input.Class;
            student.OriginOfStudent = input.OriginOfStudent;
            student.Department = input.Department;
            student.ProfessionLevel = input.ProfessionLevel;

            student.EntryDate = entryDate;                //4

            student.IsEmailConfirmed = true;

            student = await _studentRepository.UpdateAsync(student);
        }


        public async Task AddStudent(AddStudentInput input)
        {
            var student = _studentRepository.FirstOrDefault(x => x.UserName == input.LoginId);
            if (student != null) throw new UserFriendlyException($"登陆账号{input.StudentNo}已存在！");

            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.CourseSelecting.Student);
            if (role == null) throw new UserFriendlyException("系统未初始化！");


            #region 根据学号处理入学时间年级
            if (input.LoginId.Length < 4) throw new UserFriendlyException("学号格式不正确");
            var yearstr = input.LoginId.Substring(0, 4);
            int year;
            try
            {
                year = Convert.ToInt32(yearstr);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("学号格式不正确");
            }
            var entryDate = new DateTime(year, 8, 20);
            var grade = Math.Min(((DateTime.Today.Year - entryDate.Year) * 12 + (DateTime.Today.Month - entryDate.Month)) / 12, 4);
            string gradestr;
            switch (grade)
            {
                case 0:
                    gradestr = "一年级";
                    break;
                case 1:
                    gradestr = "二年级";
                    break;
                case 2:
                    gradestr = "三年级";
                    break;
                case 3:
                    gradestr = "四年级";
                    break;
                case 4:
                    gradestr = "五年级";
                    break;
                default:
                    gradestr = "五年级";
                    break;
            }
            #endregion

            student = new Student
            {
                TenantId = AbpSession.TenantId,
                Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword),
                UserName = input.LoginId,
                Name = input.StudentName,
                Surname = input.StudentName,
                EmailAddress = "53590040@qq.com",
                Department = input.Department,
                Major = input.Major,
                StudentNo = input.StudentNo,

                EntryDate = entryDate,                //4
                Grade = gradestr

                //TenantId = AbpSession.TenantId,
                //UserName = input.LoginId,
                //StudentNo = input.StudentNo,
                //Name = input.StudentName,
                //Department = input.Department,
                //Major = input.Major,
                //Gender = input.Gebder
            };

            student = await _studentRepository.InsertAsync(student);

            var userRole = new UserRole
            {
                TenantId = AbpSession.TenantId,
                RoleId = role.Id,
                // UserId = student.Id
            };

            await _userRoleRepository.InsertAsync(userRole);
        }

        public async Task EditStudent(EditStudentInput input)
        {
            var student = _studentRepository.FirstOrDefault(x => x.Id == input.Id);
            if (student == null) throw new UserFriendlyException($"学号{input.LoginId}不存在！");

            if (input.LoginId.ToString() != student.UserName)
            {
                var existsStudent = _studentRepository.FirstOrDefault(x => x.UserName == input.LoginId.ToString());
                if (existsStudent != null) throw new UserFriendlyException($"学号{input.LoginId}已存在！");
            }


            #region 根据学号处理入学时间年级
            if (input.LoginId.Length < 4) throw new UserFriendlyException("学号格式不正确");
            var yearstr = input.LoginId.Substring(0, 4);
            int year;
            try
            {
                year = Convert.ToInt32(yearstr);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("学号格式不正确");
            }
            var entryDate = new DateTime(year, 8, 20);
            var grade = Math.Min(((DateTime.Today.Year - entryDate.Year) * 12 + (DateTime.Today.Month - entryDate.Month)) / 12, 4);
            string gradestr;
            switch (grade)
            {
                case 0:
                    gradestr = "一年级";
                    break;
                case 1:
                    gradestr = "二年级";
                    break;
                case 2:
                    gradestr = "三年级";
                    break;
                case 3:
                    gradestr = "四年级";
                    break;
                case 4:
                    gradestr = "五年级";
                    break;
                default:
                    gradestr = "五年级";
                    break;
            }
            #endregion

            student.Name = input.StudentName;
            student.Surname = input.StudentName;
            //student.UserName = input.StudentNo;              //2
            student.UserName = input.LoginId;
            student.Grade = gradestr;

            student.Department = input.Department;
            student.Major = input.Major;
            student.Gender = input.Gender;
            student.EntryDate = entryDate;            //4
            // student.StudentNo = input.StudentNo;        //2

            await _studentRepository.UpdateAsync(student);
        }

        public StudentDetailsDto GetStudent(long id)
        {
            var student = _studentRepository.FirstOrDefault(x => x.Id == id);
            return student.MapTo<StudentDetailsDto>();
        }

        [UnitOfWork]
        public async Task DeleteStudent(long id)
        {
            await _studentExamTimeRepository.DeleteAsync(x => x.StudentId == id);

            await _studentCourseTimeRepository.DeleteAsync(x => x.StudentId == id);

            await _studentSubjectProjectRepository.DeleteAsync(x => x.StudentId == id);

            await _studentRepository.DeleteAsync(id);

            UnitOfWorkManager.Current.SaveChanges();
        }

        //ListResultOutput<StudentListDto> IStudentAppService.GetStudents(GetStudentsInput input)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task ResetStudent(long id)
        {
            //await _courseTimeRepository.ResetAsync(x => x.TeacherCourse.TeacherId == id);
            //await _teacherCourseRepository.ResetAsync(x => x.TeacherId == id);
            var student = await _studentRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) throw new UserFriendlyException("当前学生不存在，请重新登录。");

            student.Password = new PasswordHasher().HashPassword(Teacher.DefaultTeacherPassword);

            await _studentRepository.UpdateAsync(student);

        }


    }
}
