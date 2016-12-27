using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using CourseSelecting.EntityFramework;

namespace CourseSelecting
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(CourseSelectingCoreModule))]
    public class CourseSelectingDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CourseSelectingDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
