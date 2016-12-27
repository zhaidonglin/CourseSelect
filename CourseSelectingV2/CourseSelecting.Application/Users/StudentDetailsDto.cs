using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace CourseSelecting.Users.Dto
{
    //public class StudentDetailsDto
    //{
    //}
    [AutoMapFrom(typeof(Student))]
    public class StudentDetailsDto : EntityDto<long>
    {
        public string StudentNo { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
        public string Major { get; set; }
        public string Gender { get; set; }

        public string LoginId { get; set; }


        public string UserName { get; set; }

        //public string StudentId { get;  set; }
        public string StudentName { get; set; }
     
        public string Tel { get; set; }
        public string Grade { get; set; }
        public string Class { get; set; }
        public string OriginOfStudent { get; set; }
        public string ProfessionLevel { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public  DateTime EntryDate { get; set; }
    }
}