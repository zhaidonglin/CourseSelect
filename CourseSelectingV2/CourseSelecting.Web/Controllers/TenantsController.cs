using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CourseSelecting.Authorization;
using CourseSelecting.MultiTenancy;

namespace CourseSelecting.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : CourseSelectingControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}