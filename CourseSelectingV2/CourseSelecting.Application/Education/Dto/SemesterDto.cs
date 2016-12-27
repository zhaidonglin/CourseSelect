using System;
using Abp.AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(Semester))]
    public class SemesterDto
    {
        public string Name { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
    }
}
