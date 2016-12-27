using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CourseSelecting.Authorization;
using Newtonsoft.Json;
using CourseSelecting.Web.Models.Manage;
using Abp.UI;
using CourseSelecting.Users;
using Abp.Threading;
using CourseSelecting.Education;

namespace CourseSelecting.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Students)]
    public class StudentController : CourseSelectingControllerBase
    {
        private readonly IStudentAppService _studentService;
        private readonly ISubjectProjectAppService _subjectService;

        public StudentController(IStudentAppService studentService,
            ISubjectProjectAppService subjectService)
        {
            _studentService = studentService;
            _subjectService = subjectService;
        }
        // GET: Student
        public ActionResult Index()
        {
            var teacher = _studentService.GetStudent(AbpSession.UserId ?? 0);
            if (!teacher.IsEmailConfirmed) return RedirectToAction("SelfInfo");

            return RedirectToAction("CourseTimeTables");
        }

        public ActionResult SelfInfo()
        {
            var student = _studentService.GetStudent(AbpSession.UserId ?? 0);
            if (student == null) throw new UserFriendlyException("请求参数错误。");

            return View(new StudentDetailsModel
            {
                Student = student
            });
        }

        public ActionResult SelfResetPwd()
        {
            return View();
        }

        public ActionResult CourseSelect()
        {
            return View();
        }
        public ActionResult SubjectProjectCourses(int id)
        {

            var subject = AsyncHelper.RunSync(() => _subjectService.GetSubjectProjectCourses(id));
            return View(new SubjectProjectCoursesModel
            {
                SubjectProject = subject
            });
        }
        public ActionResult CourseTimeTables()
        {
            return View();
        }

        public ActionResult StudentExamTimes()
        {
            return View();
        }
        public ActionResult CreditInfo()
        {
            return View();
        }
        public ActionResult StudedntCourseTimeTables()
        {
            return View();
        }
        public ActionResult JsonData(int draw, int start)
        {
            var request = Request;


            var list = new[]
            {
                new {Id = start + 1, teacherNo = "0010", teacherName = "焦晓辉", department = "临床科", loginId = "sfwjiao"},
                new {Id = start + 2, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 3, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 4, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 5, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 6, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 7, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 8, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 9, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
                new {Id = start + 10, teacherNo = "0011", teacherName = "张世强", department = "临床科", loginId = "joy"},
            };



            return Content(JsonConvert.SerializeObject(new
            {
                draw = draw,
                recordsTotal = 100,
                recordsFiltered = 100,
                data = list
            }));
        }
        }
}