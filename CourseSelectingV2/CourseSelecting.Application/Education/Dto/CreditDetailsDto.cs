using Abp.AutoMapper;
using CourseSelecting.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(StudentSubjectProject))]
    public class CreditDetailsDto
    {
        public int Id { get; set; }

        public SubjectProjectSimpleDto SubjectProject { get; set; }

        public StudentDetailsDto Student { get; set; }

        public string ClassCredit { get; set; }

        public string ExamCredit { get; set; }

        public string SubjectCredit { get; set; }
    }
}
