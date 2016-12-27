using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CourseSelecting.Authorization
{
    public class CourseSelectingAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var admins = pages.CreateChildPermission(PermissionNames.Pages_Managers, L("Admins"));

            var teachers = pages.CreateChildPermission(PermissionNames.Pages_Teachers, L("Teachers"));

            var students = pages.CreateChildPermission(PermissionNames.Pages_Students, L("Students"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CourseSelectingConsts.LocalizationSourceName);
        }
    }
}
