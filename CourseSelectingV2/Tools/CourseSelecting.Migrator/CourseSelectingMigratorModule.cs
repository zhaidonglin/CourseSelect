using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using CourseSelecting.EntityFramework;

namespace CourseSelecting.Migrator
{
    [DependsOn(typeof(CourseSelectingDataModule))]
    public class CourseSelectingMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<CourseSelectingDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}