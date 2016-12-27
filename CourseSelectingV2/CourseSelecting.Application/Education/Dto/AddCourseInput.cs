using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseSelecting.Users;

namespace CourseSelecting.Education.Dto
{
    public class AddCourseInput
    {
        public int SubjectProjectId { get; set; }

        public string CourseName { get; set; }
        public string CourseGrade { get; set; }

        public double CourseCredit { get; set; }

        public int CourseLimitNumbers { get; set; }

        public string Remark { get; set; }
        
    }
}

