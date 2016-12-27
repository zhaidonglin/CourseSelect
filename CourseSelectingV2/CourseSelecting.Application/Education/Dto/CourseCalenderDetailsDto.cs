using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelecting.Users.Dto
{
    [AutoMapFrom(typeof(Education.Course))]
    public class CourseCalenderDetailsDto : Abp.Application.Services.Dto.EntityDto<int>
    {
        public string TeacherId { get; set; }

        public string Name { get; set; }
              
        public int LimitNumbers { get; set; }

        public int SelectedNumbers { get; set; }
    }
}
