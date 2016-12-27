namespace CourseSelecting.Education.Dto
{
    public class EditCourseInput
    {
        public int Id { get; set; }
        public int SubjectProjectId { get; set; }
        public string CourseName { get; set; }
        public double CourseCredit { get; set; }
        public int CourseLimitNumbers { get; set; }
        public string Remark { get; set; }
    }
}
