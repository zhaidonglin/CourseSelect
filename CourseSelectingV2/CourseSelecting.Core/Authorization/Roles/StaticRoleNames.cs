namespace CourseSelecting.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";
        }

        public static class CourseSelecting
        {
            public const string Manager = "Manager";
            public const string ManagerDisplay = "管理员";
            public const string Teacher = "Teacher";
            public const string TeacherDisplay = "教师";
            public const string Student = "Student";
            public const string StudentDisplay = "学生";
        }
    }
}