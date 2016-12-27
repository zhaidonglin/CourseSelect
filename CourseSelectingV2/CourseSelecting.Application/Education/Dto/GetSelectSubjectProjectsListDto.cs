using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using AutoMapper;

namespace CourseSelecting.Education.Dto
{
    [AutoMapFrom(typeof(SubjectProject))]
    public class GetSelectSubjectProjectsListDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string SubjectStyle { get; set; }

        public string Type { get; set; }

        public double Credit { get; set; }

        public string AimedAt { get; set; }

        public bool IsCompulsory { get; set; }

        public string TeachingStyle { get; set; }

        public bool IsSelected { get; set; }
    }

    public class GetSelectSubjectProjectsListDtoProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SubjectProject, GetSelectSubjectProjectsListDto>()
                .ForMember(x => x.IsSelected, opt => opt.Ignore());
        }
    }
}
