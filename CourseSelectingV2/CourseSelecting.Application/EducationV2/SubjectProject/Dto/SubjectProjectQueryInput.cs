using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.SubjectProject.Dto
{
    public class SubjectProjectQueryInput : QueryInput
    {
        public int? SemesterId { get; set; }
        public int? Id { get; set; }
    }
}
