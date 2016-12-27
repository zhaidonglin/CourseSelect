using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Users.Dto
{
    public class AddStudentInput
    {
        public string Major { get; set; }

        public string StudentNo { get; set; }
        public string StudentName { get;  set; }
      
        public string Department { get; set; }
        
        public string LoginId { get; set; }

        public  DateTime EntryDate { get; set; }        //4
    }
}
