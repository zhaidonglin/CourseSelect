using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using CourseSelecting.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace CourseSelecting.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CourseSelecting.EntityFramework.CourseSelectingDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CourseSelecting";
        }

        protected override void Seed(CourseSelecting.EntityFramework.CourseSelectingDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
