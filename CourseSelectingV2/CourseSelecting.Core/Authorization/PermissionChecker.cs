using Abp.Authorization;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.MultiTenancy;
using CourseSelecting.Users;

namespace CourseSelecting.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
