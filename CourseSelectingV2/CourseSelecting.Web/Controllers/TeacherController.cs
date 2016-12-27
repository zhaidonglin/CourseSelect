using System.Web.Mvc;
using Abp.Threading;
using Abp.Web.Mvc.Authorization;
using CourseSelecting.Authorization;
using CourseSelecting.Education;
using CourseSelecting.Web.Models.Teacher;
using CourseSelecting.Users;
using Abp.UI;
using CourseSelecting.Application.EducationV2.TeacherCourse;
using CourseSelecting.Application.EducationV2.TeacherCourse.Dto;
using CourseSelecting.Web.Models.Manage;

namespace CourseSelecting.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Teachers)]
    public class TeacherController : CourseSelectingControllerBase
    {
        private readonly ITeacherAppService _teacherService;
        private readonly ICourseAppService _courseService;
        private readonly ITeacherCourseV2AppService _teacherCourseService;


        // GET: Teacher
        public TeacherController(
            ICourseAppService courseService,
            ITeacherAppService teacherService,
            ITeacherCourseV2AppService teacherCourseService
            )
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _teacherCourseService = teacherCourseService;
        }
        public ActionResult Index()
        {
            var teacher = _teacherService.GetTeacher(AbpSession.UserId ?? 0);
            if (!teacher.IsEmailConfirmed) return RedirectToAction("SelfInfo");

            return RedirectToAction("CourseTimeTables");
        }

        public ActionResult FirstLogin()
        {
            return View();
        }

        public ActionResult SelfInfo()
        {
            var teacher = _teacherService.GetTeacher(AbpSession.UserId ?? 0);
            if (teacher == null) throw new UserFriendlyException("请求参数错误。");

            return View(new TeacherDetailsModel
            {
                Teacher = teacher
            });
        }

        public ActionResult SelfResetPwd()
        {
            return View();
        }

        public ActionResult CourseTimeTables()
        {
            var teacherCourses = AsyncHelper.RunSync(() => _courseService.GetTeacherCourses(AbpSession.UserId ?? 0));

            return View(new AddCourseTimeTablesModel
            {
                TeacherCourses = teacherCourses
            });
        }

        public ActionResult Exam()
        {
            if (!AbpSession.UserId.HasValue) throw new UserFriendlyException("用户未登录，请重新登录。");

            var teacherCourses = AsyncHelper.RunSync(
                () => _teacherCourseService.Query(new TeacherCourseQueryInput
                {
                    PageSize = 0,
                    Start = 0,
                    TeacherId = AbpSession.UserId
                }));

            return View(new ExamViewModel
            {
                TeacherCourse = teacherCourses
            });
        }

        public ActionResult ExamAdd()
        {
            return View();
        }

        public ActionResult ExamEdit()
        {
            return View();
        }

        public ActionResult CreditManage()
        {
            return View();
        }

        /// <summary>
        /// 获取当前已选该课程的学生
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCourseTimeOrderdStudents(int id)
        {
            ViewBag.CourseTimeId = id;
            return View();
        }

        /// <summary>
        /// 获取当前已选该考试的学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetExamTimeOrderdStudents(int id)
        {
            ViewBag.ExamTimeId = id;
            return View();
        }
    }
}