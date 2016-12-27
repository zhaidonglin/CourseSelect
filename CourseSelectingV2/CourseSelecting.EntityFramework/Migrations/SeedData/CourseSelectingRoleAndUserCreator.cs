using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using CourseSelecting.Authorization;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.EntityFramework;
using CourseSelecting.Users;
using Microsoft.AspNet.Identity;

namespace CourseSelecting.Migrations.SeedData
{
    public class CourseSelectingRoleAndUserCreator
    {
        private readonly CourseSelectingDbContext _context;

        public CourseSelectingRoleAndUserCreator(CourseSelectingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCourseSelectingRoleAndUsers();
        }

        private void CreateCourseSelectingRoleAndUsers()
        {
            //Manager role for host

            var managerRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.CourseSelecting.Manager);
            if (managerRoleForHost == null)
            {
                managerRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.CourseSelecting.Manager, DisplayName = StaticRoleNames.CourseSelecting.ManagerDisplay, IsStatic = true });
                _context.SaveChanges();

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages,
                    IsGranted = true,
                    RoleId = managerRoleForHost.Id
                });

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages_Managers,
                    IsGranted = true,
                    RoleId = managerRoleForHost.Id
                });

                _context.SaveChanges();
            }

            //Manager user for tenancy host

            var managerUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.ManagerLoginId);
            if (managerUserForHost == null)
            {
                managerUserForHost = _context.Users.Add(
                    new User
                    {
                        UserName = User.ManagerLoginId,
                        Name = User.ManagerName,
                        Surname = User.ManagerName,
                        EmailAddress = User.SystemEmail,
                        IsEmailConfirmed = true,
                        Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                    });

                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(null, managerUserForHost.Id, managerRoleForHost.Id));

                _context.SaveChanges();
            }

            //Teacher role for host

            var teacherRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.CourseSelecting.Teacher);
            if (teacherRoleForHost == null)
            {
                teacherRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.CourseSelecting.Teacher, DisplayName = StaticRoleNames.CourseSelecting.TeacherDisplay, IsStatic = true });
                _context.SaveChanges();

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages,
                    IsGranted = true,
                    RoleId = teacherRoleForHost.Id
                });

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages_Teachers,
                    IsGranted = true,
                    RoleId = teacherRoleForHost.Id
                });

                _context.SaveChanges();
            }

            //Student role for host

            var studentRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.CourseSelecting.Student);
            if (studentRoleForHost == null)
            {
                studentRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.CourseSelecting.Student, DisplayName = StaticRoleNames.CourseSelecting.StudentDisplay, IsStatic = true });
                _context.SaveChanges();

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages,
                    IsGranted = true,
                    RoleId = studentRoleForHost.Id
                });

                _context.Permissions.Add(new RolePermissionSetting
                {
                    TenantId = null,
                    Name = PermissionNames.Pages_Students,
                    IsGranted = true,
                    RoleId = studentRoleForHost.Id
                });

                _context.SaveChanges();
            }
        }
    }
}
