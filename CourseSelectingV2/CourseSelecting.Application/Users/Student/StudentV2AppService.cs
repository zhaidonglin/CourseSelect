using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CourseSelecting.Application.Users.Student.Dto;
using Abp.Domain.Repositories;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.UI;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.Users;
using Microsoft.AspNet.Identity;
using Abp.AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace CourseSelecting.Application.Users.Student
{
    public class StudentV2AppService : CourseSelectingAppServiceBase, IStudentV2AppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<CourseSelecting.Users.Student, long> _studentRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly RoleManager _roleManager;
        public StudentV2AppService(
            IRepository<User, long> userRepository,
            IRepository<CourseSelecting.Users.Student, long> studentRepository,
            RoleManager roleManage,
            IRepository<UserRole, long> userRoleRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManage;
        }

        public async Task Add(StudentInput input)
        {
            //验证传入参数
            if (!input.EntryDate.HasValue) throw new UserFriendlyException("传入EntryDate参数不正确！");

            //验证用户名是否存在
            var user = _userRepository.FirstOrDefault(x => x.UserName == input.StudentNo);
            if (user != null) throw new UserFriendlyException($"登陆账号{input.StudentNo}已存在！");

            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.CourseSelecting.Student);
            if (role == null) throw new UserFriendlyException("系统未初始化！");

            //创建学生对象
            var student = new CourseSelecting.Users.Student
            {
                TenantId = AbpSession.TenantId,
                UserName = input.StudentNo,
                //用户名称
                Name = input.Name,
                Surname = input.Name,
                EmailAddress = "53590040@qq.com",
                //入学时间
                EntryDate = input.EntryDate.Value,
                StudentNo = input.StudentNo

            };
            #region --非必填属性赋值--

            //密码属性赋值
            if (!string.IsNullOrEmpty(input.Password))
            {
                student.Password = new PasswordHasher().HashPassword(input.Password);
            }
            else
            {
                student.Password = new PasswordHasher().HashPassword(CourseSelecting.Users.Student.DefaultStudentPassword);
            }

            //性别属性赋值
            if (input.Gender.HasValue) student.Gender = input.Gender.Value;
            //专业属性赋值
            if (!string.IsNullOrEmpty(input.Major)) student.Major = input.Major;
            //电话属性赋值
            if (!string.IsNullOrEmpty(input.Tel)) student.Tel = input.Tel;
            //年级属性赋值
            if (!string.IsNullOrEmpty(input.Grade)) student.Grade = input.Grade;
            //班级属性赋值
            if (!string.IsNullOrEmpty(input.Class)) student.Class = input.Class;
            //生源地属性赋值
            if (!string.IsNullOrEmpty(input.OriginOfStudent)) student.OriginOfStudent = input.OriginOfStudent;
            //专业等级属性赋值
            if (!string.IsNullOrEmpty(input.ProfessionLevel)) student.ProfessionLevel = input.ProfessionLevel;
            //院系属性赋值
            if (!string.IsNullOrEmpty(input.Department)) student.Department = input.Department;
            
            #endregion

            student = await _studentRepository.InsertAsync(student);

            var userRole = new UserRole
            {
                TenantId = AbpSession.TenantId,
                RoleId = role.Id,
                UserId = student.Id
            };

            await _userRoleRepository.InsertAsync(userRole);
        }

        public async Task Edit(StudentInput input)
        {
            var student = _studentRepository.FirstOrDefault(x => x.Id == input.Id);
            if (student == null) throw new UserFriendlyException($"登陆账号{input.StudentNo}不存在！");
            
            //验证用户名是否重复
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.StudentNo && x.Id != student.Id);
            if (user != null) throw new UserFriendlyException($"登陆账号{input.StudentNo}已存在！");


            if (!string.IsNullOrEmpty(input.StudentNo)) student.UserName = input.StudentNo;
            if (!string.IsNullOrEmpty(input.Name)) student.Name = input.Name;
            //入学时间
            if (input.EntryDate.HasValue) student.EntryDate = input.EntryDate.Value;
            #region --非必填属性赋值--

            //密码属性赋值
            if (!string.IsNullOrEmpty(input.Password))
            {
                student.Password = new PasswordHasher().HashPassword(input.Password);
            }
            else
            {
                student.Password = new PasswordHasher().HashPassword(CourseSelecting.Users.Student.DefaultStudentPassword);
            }

            //性别属性赋值
            if (input.Gender.HasValue) student.Gender = input.Gender.Value;
            //专业属性赋值
            if (!string.IsNullOrEmpty(input.Major)) student.Major = input.Major;
            //电话属性赋值
            if (!string.IsNullOrEmpty(input.Tel)) student.Tel = input.Tel;
            //年级属性赋值
            if (!string.IsNullOrEmpty(input.Grade)) student.Grade = input.Grade;
            //班级属性赋值
            if (!string.IsNullOrEmpty(input.Class)) student.Class = input.Class;
            //生源地属性赋值
            if (!string.IsNullOrEmpty(input.OriginOfStudent)) student.OriginOfStudent = input.OriginOfStudent;
            //专业等级属性赋值
            if (!string.IsNullOrEmpty(input.ProfessionLevel)) student.ProfessionLevel = input.ProfessionLevel;
            //院系属性赋值
            if (!string.IsNullOrEmpty(input.Department)) student.Department = input.Department;

            #endregion
            await _studentRepository.UpdateAsync(student);
        }

        public async Task Delete(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }

        public async Task<StudentDto> Get(int id)
        {
            var result = await _studentRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentDto>();
        }

        public async Task<StudentDto> Get(StudentQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentDto>();
        }

        public async Task<StudentSimpleDto> Simple(int id)
        {
            var result = await _studentRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSimpleDto>();
        }

        public async Task<StudentSimpleDto> Simple(StudentQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _studentRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<StudentSimpleDto>();
        }

        public async Task<PagedResultDto<StudentDto>> Query(StudentQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取总数
            var totalcount = await _studentRepository.CountAsync();

            //获取查询对象
            var query = _studentRepository.GetAll();

            //添加关键词条件
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                query = query.Where(x =>
                    x.Name.Contains(input.KeyWord)
                );
            }

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<StudentDto>(totalcount, courses.MapTo<List<StudentDto>>());
        }
    }
}
