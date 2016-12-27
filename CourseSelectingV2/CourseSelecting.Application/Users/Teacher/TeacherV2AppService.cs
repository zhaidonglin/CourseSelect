using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CourseSelecting.Application.Users.Teacher.Dto;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.Users;
using Microsoft.AspNet.Identity;

namespace CourseSelecting.Application.Users.Teacher
{
    public class TeacherV2AppService : CourseSelectingAppServiceBase, ITeacherV2AppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<CourseSelecting.Users.Teacher, long> _teacherRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public TeacherV2AppService(
            IRepository<User, long> userRepository,
            IRepository<CourseSelecting.Users.Teacher, long> teacherRepository,
            RoleManager roleManage,
            IRepository<UserRole, long> userRoleRepository
            )
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _roleManager = roleManage;
            _userRoleRepository = userRoleRepository;
        }

        public async Task Add(TeacherInput input)
        {
            //验证用户名是否重复
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.UserName);
            if (user != null) throw new UserFriendlyException($"登陆账号{input.UserName}已存在！");

            //验证系统是否存在教师角色
            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.CourseSelecting.Teacher);
            if (role == null) throw new UserFriendlyException("系统未初始化！");

            //创建教师对象
            var teacher = new CourseSelecting.Users.Teacher
            {
                TenantId = AbpSession.TenantId,
                UserName = input.UserName,
                Name = input.Name,
                Surname = input.Name,
                EmailAddress = "53590040@qq.com",
                TeacherNo = input.TeacherNo,
                Department = input.Department
            };

            #region --非必填属性赋值--

            //密码属性赋值
            if (!string.IsNullOrEmpty(input.Password))
            {
                teacher.Password = new PasswordHasher().HashPassword(input.Password);
            }
            else
            {
                teacher.Password = new PasswordHasher().HashPassword(CourseSelecting.Users.Teacher.DefaultTeacherPassword);
            }

            //性别属性赋值
            if (input.Gender.HasValue) teacher.Gender = input.Gender.Value;

            //专业属性赋值
            if (!string.IsNullOrEmpty(input.Major)) teacher.Major = input.Major;

            //学历属性赋值
            if (!string.IsNullOrEmpty(input.Diploma)) teacher.Diploma = input.Diploma;

            //学位属性赋值
            if (!string.IsNullOrEmpty(input.Degree)) teacher.Degree = input.Degree;

            //职称属性赋值
            if (!string.IsNullOrEmpty(input.PositionalTitle)) teacher.PositionalTitle = input.PositionalTitle;

            //电话属性赋值
            if (!string.IsNullOrEmpty(input.Tel)) teacher.Tel = input.Tel;

            //工作年限属性赋值
            if (!string.IsNullOrEmpty(input.YearsOfWorking)) teacher.YearsOfWorking = input.YearsOfWorking;

            //教学年限属性赋值
            if (!string.IsNullOrEmpty(input.YearsOfTeaching)) teacher.YearsOfTeaching = input.YearsOfTeaching;

            //是否是专职属性赋值
            if (input.IsFullTime.HasValue) teacher.IsFullTime = input.IsFullTime.Value;

            #endregion

            //执行插入教师数据方法
            teacher = await _teacherRepository.InsertAsync(teacher);

            //创建角色对象，并赋予用户教师的角色
            var userRole = new UserRole
            {
                TenantId = AbpSession.TenantId,
                RoleId = role.Id,
                UserId = teacher.Id
            };

            //执行插入角色数据的方法
            await _userRoleRepository.InsertAsync(userRole);
        }

        public async Task Edit(TeacherInput input)
        {
            //检查Id参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            //获取需要修改的对象
            var teacher = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (teacher == null) throw new UserFriendlyException("当前教师不存在！");

            //验证用户名是否重复
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserName == input.UserName && x.Id != teacher.Id);
            if (user != null) throw new UserFriendlyException($"登陆账号{input.UserName}已存在！");
            
            //用户名属性赋值
            if (!string.IsNullOrEmpty(input.UserName)) teacher.UserName = input.UserName;

            //姓名属性赋值
            if (!string.IsNullOrEmpty(input.Name)) teacher.Name = input.Name;

            //教师工号属性赋值
            if (!string.IsNullOrEmpty(input.TeacherNo)) teacher.TeacherNo = input.TeacherNo;

            //部门属性赋值
            if (!string.IsNullOrEmpty(input.Department)) teacher.Department = input.Department;

            //密码属性赋值
            if (input.IsUpdatePassword.HasValue && input.IsUpdatePassword.Value)
            {
                if (!string.IsNullOrEmpty(input.Password))
                {
                    teacher.Password = new PasswordHasher().HashPassword(input.Password);
                }
                else
                {
                    teacher.Password = new PasswordHasher().HashPassword(CourseSelecting.Users.Teacher.DefaultTeacherPassword);
                }
            }

            //性别属性赋值
            if (input.Gender.HasValue) teacher.Gender = input.Gender.Value;

            //专业属性赋值
            if (!string.IsNullOrEmpty(input.Major)) teacher.Major = input.Major;

            //学历属性赋值
            if (!string.IsNullOrEmpty(input.Diploma)) teacher.Diploma = input.Diploma;

            //学位属性赋值
            if (!string.IsNullOrEmpty(input.Degree)) teacher.Degree = input.Degree;

            //职称属性赋值
            if (!string.IsNullOrEmpty(input.PositionalTitle)) teacher.PositionalTitle = input.PositionalTitle;

            //电话属性赋值
            if (!string.IsNullOrEmpty(input.Tel)) teacher.Tel = input.Tel;

            //工作年限属性赋值
            if (!string.IsNullOrEmpty(input.YearsOfWorking)) teacher.YearsOfWorking = input.YearsOfWorking;

            //教学年限属性赋值
            if (!string.IsNullOrEmpty(input.YearsOfTeaching)) teacher.YearsOfTeaching = input.YearsOfTeaching;

            //是否是专职属性赋值
            if (input.IsFullTime.HasValue) teacher.IsFullTime = input.IsFullTime.Value;
            
            //执行修改教师数据方法
            teacher = await _teacherRepository.UpdateAsync(teacher);
        }

        public async Task Delete(int id)
        {
            await _teacherRepository.DeleteAsync(id);
        }

        public async Task<TeacherDto> Get(int id)
        {
            var result = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherDto>();
        }

        public async Task<TeacherDto> Get(TeacherQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherDto>();
        }

        public async Task<TeacherSimpleDto> Simple(int id)
        {
            var result = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherSimpleDto>();
        }

        public async Task<TeacherSimpleDto> Simple(TeacherQueryInput input)
        {
            //检查传入参数
            if (!input.Id.HasValue) throw new UserFriendlyException("传入Id参数不正确！");

            var result = await _teacherRepository.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            if (result == null) throw new UserFriendlyException("该条信息不存在！");

            return result.MapTo<TeacherSimpleDto>();
        }

        public async Task<PagedResultDto<TeacherDto>> Query(TeacherQueryInput input)
        {
            //验证参数
            if (!input.PageSize.HasValue) throw new UserFriendlyException("传入PageSize参数不正确！");
            if (!input.Start.HasValue) throw new UserFriendlyException("传入Start参数不正确！");

            //获取总数
            var totalcount = await _teacherRepository.CountAsync();

            //获取查询对象
            var query = _teacherRepository.GetAll();

            //添加分页条件
            query = query.OrderBy(x => x.CreationTime)
                .Skip(input.Start.Value).Take(input.PageSize.Value);

            //执行查询
            var courses = await Task.FromResult(query.ToList());

            //包装为分页输出对象
            return new PagedResultOutput<TeacherDto>(totalcount, courses.MapTo<List<TeacherDto>>());
        }
    }
}
