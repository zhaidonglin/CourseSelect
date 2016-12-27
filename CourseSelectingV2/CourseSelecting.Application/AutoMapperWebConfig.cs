using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseSelecting.Education.Dto;

namespace CourseSelecting
{
    public static class AutoMapperWebConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<GetSelectSubjectProjectsListDtoProfile>();
            });
            Mapper.AssertConfigurationIsValid();//验证所有的映射配置是否都正常
        }
    }
}
