using CourseSelecting.EntityFramework;
using EntityFramework.DynamicFilters;

namespace CourseSelecting.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly CourseSelectingDbContext _context;

        public InitialHostDbBuilder(CourseSelectingDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new CourseSelectingRoleAndUserCreator(_context).Create();
        }
    }
}
