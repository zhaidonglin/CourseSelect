namespace CourseSelecting.Application.Users.Teacher.Dto
{
    public class TeacherInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 教师学号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 院系
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 性别（0：男，1：女）
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string Major { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Diploma { get; set; }

        /// <summary>
        /// 学位
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        public string PositionalTitle { get; set; }

        /// <summary>
        /// 电话/联系方式
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 工作年限
        /// </summary>
        public string YearsOfWorking { get; set; }

        /// <summary>
        /// 教学年限
        /// </summary>
        public string YearsOfTeaching { get; set; }

        /// <summary>
        /// 是否是专职
        /// </summary>
        public bool? IsFullTime { get; set; }
        
        /// <summary>
        /// 登录Id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否修改密码
        /// </summary>
        public bool? IsUpdatePassword { get; set; }
    }
}
