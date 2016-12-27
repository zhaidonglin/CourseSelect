using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class EditCreditsInput
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public double ClassCredit { get; set; }
        public double ExamCredit { get; set; }
        public double SubjectCredit { get; set; }
    }
}
