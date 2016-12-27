using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class GetCourseTimeEventInput
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
