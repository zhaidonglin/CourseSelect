namespace CourseSelecting.Education.Dto
{
    public class GetSubjectProjectsInput
    {
        public int PageSize { get; set; }

        public int Start { get; set; }

        public long? studentId { get; set; }
        public string KeyWord { get; set; }
    }
}
