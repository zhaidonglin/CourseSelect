using System.Web.Mvc;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using CourseSelecting.Authorization;
using CourseSelecting.Education;
using CourseSelecting.Users;
using CourseSelecting.Web.Models.Manage;
using Newtonsoft.Json;
using CourseSelecting.Education.Dto;
using Abp.Application.Services.Dto;

namespace CourseSelecting.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Managers)]
    public class ManageController : CourseSelectingControllerBase
    {
        private readonly ITeacherAppService _teacherService;
        private readonly ICourseAppService _courseService;
        private readonly ISubjectProjectAppService _subjectService;
        private readonly IStudentAppService _studentService;

        public ManageController(ITeacherAppService teacherService,
            ICourseAppService courseService,
            ISubjectProjectAppService subjectService,
            IStudentAppService studentService)
        {
            _teacherService = teacherService;
            _courseService = courseService;
            _subjectService = subjectService;
            _studentService = studentService;
        }

        #region 欢迎页

        // GET: Manage
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Teacher");
        }

        #endregion

        #region 教师管理

        public ActionResult Teacher()
        {
            return View();
        }

        public ActionResult TeacherAdd()
        {
            return View();
        }

        public ActionResult TeacherEdit(long id)
        {
            var teacher = _teacherService.GetTeacher(id);
            if (teacher == null) throw new UserFriendlyException("请求参数错误。");

            return View(new TeacherDetailsModel
            {
                Teacher = teacher
            });
        }

        /// <summary>
        /// 教师负责的课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TeacherCourseTables(long id)
        {
            ViewBag.TeacherId = id;
            return View();
        }

        //public ActionResult StudentCourseTables(long id)
        //{
        //    var student = _studentService.GetStudent(id);
        //    if (student == null) throw new UserFriendlyException("请求参数错误。");

        //    return View(new StudentDetailsModel
        //    {
        //        Student = student                                         //10
        //    });
        //}

        public ActionResult StudentCourseTables(int id)                //10
        {
            //var subjects = AsyncHelper.RunSync(() => _teacherService.GetCourseTime(id));
            //var student = _studentService.GetStudent(id);
            //var course = AsyncHelper.RunSync(() => _courseService.GetCourse(id));
            //var coursetime = AsyncHelper.RunSync(() => _teacherService.GetCourseTime(id));

            //if (coursetime == null) throw new UserFriendlyException("请求参数错误。");

            return View(new CourseTimeModel());
        }

        public ActionResult TeacherDetails(long id)
        {
            var teacher = _teacherService.GetTeacher(id);
            if (teacher == null) throw new UserFriendlyException("请求参数错误。");

            var teacherCourses = AsyncHelper.RunSync(()=>_courseService.GetTeacherCourses(teacher.Id));

            var subjects = AsyncHelper.RunSync(() => _subjectService.GetSubjectProjectsWithCourses());

            return View(new TeacherCourseDetailsModel
            {
                Teacher = teacher,
                TeacherCourses = teacherCourses,
                SubjectProjectCourses = subjects
            });
        }

        #endregion

        #region 项目管理

        public ActionResult SubjectProjectAdd()
        {
            return View();
        }

        public ActionResult SubjectProject()
        {
            return View();
        }

        public ActionResult SubjectProjectEdit(int id)
        {
            var subject = AsyncHelper.RunSync(() => _subjectService.GetSubjectProject(id));
            return View(new SubjectProjectEditModel
            {
                SubjectProject = subject
            });
        }

        public ActionResult SubjectProjectCourses(int id)
        {

            var subject = AsyncHelper.RunSync(() => _subjectService.GetSubjectProjectCourses(id));
            return View(new SubjectProjectCoursesModel
            {
                SubjectProject = subject
            });
        }

        #endregion

        #region 课程管理

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult CourseAdd()
        {
            var subjects = AsyncHelper.RunSync(() => _subjectService.GetCurrentSemesterSubjectProjects());

            return View(new CourseAddModel
            {
                SubjectProjects = subjects
            });
        }

        public ActionResult CourseEdit(int id)
        {
            var subjects = AsyncHelper.RunSync(() => _subjectService.GetCurrentSemesterSubjectProjects());

            var course = AsyncHelper.RunSync(() => _courseService.GetCourse(id));
            if (course == null) throw new UserFriendlyException("请求参数错误。");

            return View(new CourseDetailsModel
            {
                SubjectProjects = subjects,
                Course = course
            });
        }

        public ActionResult GetOrderStudentTables(int? id)
        {
            ViewBag.CourseId = id;
            return View();
        }

        #endregion

        #region 学生管理

        public ActionResult Student()
        {
            return View();
        }

        

        public ActionResult StudentAdd()
        {
            return View();
        }

        //public ActionResult StudentEdit()
        //{
        //    return View(new StudentDetailsModel());
        //}

        public ActionResult StudentEdit(long id)
        {
            var student = _studentService.GetStudent(id);
            if (student == null) throw new UserFriendlyException("请求参数错误。");

            return View(new StudentDetailsModel
            {
                Student = student
            });
        }
        public ActionResult SelectStudentCourse(long id)
        {
            var student = _studentService.GetStudent(id);
            if (student == null) throw new UserFriendlyException("请求参数错误。");

            return View(new StudentDetailsModel
            {
                Student = student
            });
        }

        public ActionResult SSCourseTimeTables(long id)
        {
            var student = _studentService.GetStudent(id);
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

        #endregion

        public ActionResult CreditManage()
        {
            return View();
        }
        public ActionResult CreditEdit(int id)
        {
            //var subjects = AsyncHelper.RunSync(() => _courseService.GetCredits(GetCreditsInput input));

            var cerdit = AsyncHelper.RunSync(() => _courseService.GetCredit(id));
            return View(new CreditDetailsModel
            {
                Credit = cerdit
            });
        }


        public ActionResult OrderExamManage(int? id)
        {
            ViewBag.StudentExamTimeId = id;
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

            //var list = new[]
            //{
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //    new ArrayList{ "0010", "0010", "焦晓辉", "临床科", "sfwjiao"},
            //};

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