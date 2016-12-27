using Abp.Application.Navigation;
using Abp.Localization;
using CourseSelecting.Authorization;

namespace CourseSelecting.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class CourseSelectingNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(new MenuItemDefinition(
                    "TeacherManage",
                    L("MenuTeacherManage"),
                    url: "Manage/Teacher",
                    icon: "glyphicon glyphicon-user",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                
                .AddItem(new MenuItemDefinition(
                    "SubjectProjectManage",
                    L("MenuSubjectProjectManage"),
                    url: "Manage/SubjectProject",
                    icon: "glyphicon glyphicon-book",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "CourseManage",
                    L("MenuCourseManage"),
                    url: "Manage/Course",
                    icon: "glyphicon glyphicon-th-list",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "StudentManage",
                    L("MenuStudentManage"),
                    url: "Manage/Student",
                    icon: "fa fa-users",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "CreditManage",
                    L("MenuCreditManage"),
                    url: "Manage/CreditManage",
                    icon: "glyphicon glyphicon-pencil",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "OrderManage",
                    L("MenuOrderManage"),
                    url: "Manage/GetOrderStudentTables",
                    icon: "glyphicon glyphicon-pencil",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "OrderExamManage",
                    L("MenuOrderExamManage"),
                    url: "Manage/OrderExamManage",
                    icon: "glyphicon glyphicon-pencil",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "AdminResetPwd",
                    L("MenuAdminResetPwd"),
                    url: "Manage/SelfResetPwd",
                    icon: "glyphicon glyphicon-lock",
                    requiredPermissionName: PermissionNames.Pages_Managers
                    ))
                .AddItem(new MenuItemDefinition(
                    "TeacherCourseTimeTables",
                    L("MenuTeacherCourseTimeTables"),
                    url: "Teacher/CourseTimeTables",
                    icon: "glyphicon glyphicon-list-alt",
                    requiredPermissionName: PermissionNames.Pages_Teachers
                    ))
                .AddItem(new MenuItemDefinition(
                    "Exam",
                    L("MenuExamManage"),
                    url: "Teacher/Exam",
                    icon: "glyphicon glyphicon-list-alt",
                    requiredPermissionName: PermissionNames.Pages_Teachers
                    ))
                
                .AddItem(new MenuItemDefinition(
                    "TeacherSelfInfo",
                    L("MenuTeacherSelfInfo"),
                    url: "Teacher/SelfInfo",
                    icon: "fa fa-edit",
                    requiredPermissionName: PermissionNames.Pages_Teachers
                    ))
                .AddItem(new MenuItemDefinition(
                    "TeacherResetPwd",
                    L("MenuTeacherResetPwd"),
                    url: "Teacher/SelfResetPwd",
                    icon: "glyphicon glyphicon-lock",
                    requiredPermissionName: PermissionNames.Pages_Teachers
                    ))
                .AddItem(new MenuItemDefinition(
                    "StudentCourseSelect",
                    L("MenuStudentCourseSelect"),
                    url: "Student/CourseSelect",
                    icon: "glyphicon glyphicon-home",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                .AddItem(new MenuItemDefinition(
                    "StudentCourseTimeTables",
                    L("MenuStudentCourseTimeTables"),
                    url: "Student/CourseTimeTables",
                    icon: "glyphicon glyphicon-list-alt",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                .AddItem(new MenuItemDefinition(
                    "ExamOrdered",
                    L("MenuExamOrderedManage"),
                    url: "Student/StudentExamTimes",
                    icon: "glyphicon glyphicon-home",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                .AddItem(new MenuItemDefinition(
                    "StudentCreditInfo",
                    L("MenuStudentCreditInfo"),
                    url: "Student/CreditInfo",
                    icon: "glyphicon glyphicon-heart-empty",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                
                .AddItem(new MenuItemDefinition(
                    "StudentSelfInfo",
                    L("MenuStudentSelfInfo"),
                    url: "Student/SelfInfo",
                    icon: "glyphicon glyphicon-edit",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                .AddItem(new MenuItemDefinition(
                    "StudentResetPwd",
                    L("MenuStudentResetPwd"),
                    url: "Student/SelfResetPwd",
                    icon: "glyphicon glyphicon-lock",
                    requiredPermissionName: PermissionNames.Pages_Students
                    ))
                    ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CourseSelectingConsts.LocalizationSourceName);
        }
    }
}
