using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CourseSelecting.Education;

namespace CourseSelecting.IRepositories.Education
{
    public interface ISemesterRepository : IRepository<Semester>
    {
        Task<Semester> GetCurrentSemester();
    }
}
