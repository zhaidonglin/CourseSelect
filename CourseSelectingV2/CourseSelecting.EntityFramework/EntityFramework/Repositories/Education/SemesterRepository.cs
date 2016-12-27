using System;
using System.Threading.Tasks;
using Abp.EntityFramework;
using CourseSelecting.Education;
using CourseSelecting.IRepositories.Education;

namespace CourseSelecting.EntityFramework.Repositories.Education
{
    public class SemesterRepository : CourseSelectingRepositoryBase<Semester>, ISemesterRepository
    {
        public SemesterRepository(IDbContextProvider<CourseSelectingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Semester> GetCurrentSemester()
        {
            var semester = await FirstOrDefaultAsync(x => x.Start < DateTime.Today && x.End > DateTime.Today);

            if (semester == null)
            {
                var today = DateTime.Today;

                var firstTimeMark = new DateTime(today.Year, 2, 1);
                var secondTimeMark = new DateTime(today.Year, 8, 1);

                var startTime = firstTimeMark;
                var name = $"{today.Year}年上学期";

                if (today < firstTimeMark)
                {
                    startTime = firstTimeMark.AddMonths(-6);
                    name = $"{today.Year - 1}年下学期";
                }
                else if (today >= secondTimeMark)
                {
                    startTime = secondTimeMark;
                    name = $"{today.Year}年下学期";
                }

                semester = new Semester
                {
                    Name = name,
                    Start = startTime,
                    End = startTime.AddMonths(6)
                };
                semester = await InsertAsync(semester);
            }

            return semester;
        }
    }
}
