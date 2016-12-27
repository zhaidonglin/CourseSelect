using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class GetSSCourseTimeInput
    {
        public int PageSize { get; set; }

        public int Start { get; set; }

        public int StudentId { get; set; }
        public int TeacherId { get; set; }            //10

        public int? SubjectProjectId { get; set; }         //9

        public string KeyWord { get; set; }
    }
}
