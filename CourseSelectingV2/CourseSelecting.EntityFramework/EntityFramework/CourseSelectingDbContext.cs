using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using CourseSelecting.Authorization.Roles;
using CourseSelecting.Education;
using CourseSelecting.MultiTenancy;
using CourseSelecting.Users;

namespace CourseSelecting.EntityFramework
{
    public class CourseSelectingDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual IDbSet<Teacher> Teachers { get; set; }
        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Semester> Semesters { get; set; }
        public virtual IDbSet<CourseTime> CourseTimes { get; set; }
        public virtual IDbSet<ExamTime> ExamTimes { get; set; }
        public virtual IDbSet<StudentSubjectProject> StudentSubjectProjects { get; set; }
        public virtual IDbSet<StudentCourseTime> StudentCourseTimes { get; set; }
        public virtual IDbSet<StudentExamTime> StudentExamTimes { get; set; }
        public virtual IDbSet<TeacherCourse> TeacherCourses { get; set; }
        public virtual IDbSet<SubjectProject> SubjectProjects { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public CourseSelectingDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in CourseSelectingDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of CourseSelectingDbContext since ABP automatically handles it.
         */
        public CourseSelectingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }


        //This constructor is used in tests
        public CourseSelectingDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
