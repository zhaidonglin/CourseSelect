using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Education.Dto
{
    public class EditSubjectProjectInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubjectStyle { get; set; }
        public string Type { get; set; }
        public double Credit { get; set; }
        public bool IsCompulsory { get; set; }
        public string AimedAt { get; set; }
        public string TeachingStyle { get; set; }
        public string Discription { get; set; }
    }
}
