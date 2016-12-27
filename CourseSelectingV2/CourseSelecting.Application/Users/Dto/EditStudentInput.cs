using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CourseSelecting.Users.Dto
{
    public class EditStudentInput
    {
        public string Department { get; set; }
        public long Id { get;  set; }
        public string LoginId { get;  set; }
        //public string StudentId { get;  set; }
        public string StudentName { get;  set; }
        public string StudentNo { get;  set; }
        //public DateTime EntryDate { get; set; }

        public string Major { get; set; }
        public int Gender { get; set; }       
        public string Tel { get; set; }
        public string Grade { get; set; }
        public string Class { get; set; }
        public string OriginOfStudent { get; set; }
        public string ProfessionLevel { get; set; }
        public string Diploma { get;  set; }
        //public long Id { get; set; }
        //public string StudentNo { get; set; }
        //public string StudentId { get; set; }

        //public string StudentName { get; set; }

        //public string Department { get; set; }
        //public string Major { get; set; }
        //public string Gender { get; set; }

        //public string LoginId { get; set; }
    }
}