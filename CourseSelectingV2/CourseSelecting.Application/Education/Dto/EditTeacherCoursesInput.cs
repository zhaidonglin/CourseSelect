using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class EditTeacherCoursesInput
    {
        public long Id { get; set; }

        public List<int> CoursesIds { get; set; }
    }
}
