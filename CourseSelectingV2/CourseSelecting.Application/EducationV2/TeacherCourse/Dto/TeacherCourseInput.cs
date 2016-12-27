using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Application.EducationV2.TeacherCourse.Dto
{
    public class TeacherCourseInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int? CourseId { get; set; }

        /// <summary>
        /// 教师编号
        /// </summary>
        public long? TeacherId { get; set; }
    }
}
