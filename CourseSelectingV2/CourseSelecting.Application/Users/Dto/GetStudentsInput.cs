namespace CourseSelecting.Users
{
    public class GetStudentsInput
    {
        
            public int PageSize { get; set; }

            public int Start { get; set; }
            public string KeyWord { get; set; }

            public int CourseId { get; set; }           //10

    }
}