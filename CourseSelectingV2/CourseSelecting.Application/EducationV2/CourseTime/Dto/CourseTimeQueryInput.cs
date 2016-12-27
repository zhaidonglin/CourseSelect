namespace CourseSelecting.Application.EducationV2.CourseTime.Dto
{
    public class CourseTimeQueryInput : QueryInput
    {
        public int? Id { get; set; }

        public long? TeacherId { get; set; }
    }
}
