namespace CourseSelecting.Users.Dto
{
    public class GetTeachersInput
    {
        public int PageSize { get; set; }

        public int Start { get; set; }

        public string KeyWord { get; set; }
    }
}
