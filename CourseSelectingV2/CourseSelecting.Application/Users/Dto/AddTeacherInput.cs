namespace CourseSelecting.Users.Dto
{
    public class AddTeacherInput
    {
        public string TeacherNo { get; set; }

        public string TeacherName { get; set; }

        public string Department { get; set; }

        public string LoginId { get; set; }


        /// <summary>
        /// 个人资料
        /// </summary>
        public int? Gender { get; set; }
        public string Major { get; set; }
        public string Diploma { get; set; }
        public string Degree { get; set; }
        public string PositionalTitle { get; set; }
        public string YearsOfWorking { get; set; }
        public string YearsOfTeaching { get; set; }
        public bool? IsFullTime { get; set; }
        public string Tel { get; set; }


        public string SurName { get; set; }
    }
}
