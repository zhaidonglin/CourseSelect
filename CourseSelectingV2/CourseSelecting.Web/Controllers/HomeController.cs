using System.Web.Mvc;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using CourseSelecting.Authorization;
using CourseSelecting.Authorization.Roles;

namespace CourseSelecting.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : CourseSelectingControllerBase
    {
        public ActionResult Index()
        {
            if (AbpSession.UserId.HasValue)
            {
                var ismanage = AsyncHelper.RunSync(() => PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Managers));

                if (ismanage) return RedirectToAction("Index", "Manage");

                var isteacher = AsyncHelper.RunSync(() => PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Teachers));

                if(isteacher) return RedirectToAction("Index", "Teacher");

                var istudent = AsyncHelper.RunSync(() => PermissionChecker.IsGrantedAsync(PermissionNames.Pages_Students));

                if (istudent) return RedirectToAction("Index", "Student");
            }

            return View();
        }
    }
}