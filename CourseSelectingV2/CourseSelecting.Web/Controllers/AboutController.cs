using System.Web.Mvc;

namespace CourseSelecting.Web.Controllers
{
    public class AboutController : CourseSelectingControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}