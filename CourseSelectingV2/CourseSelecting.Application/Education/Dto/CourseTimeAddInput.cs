using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class CourseTimeAddInput
    {
        public int TeacherCourseId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Address { get; set; }
        public string Teacher { get; set; }
        public int CourseTimeId { get; set; }
        public int StudentId { get; set; }
        public string EnabledGrade { get; set; }
        public int TeacherId { get; set; }                 //10

    }
}
