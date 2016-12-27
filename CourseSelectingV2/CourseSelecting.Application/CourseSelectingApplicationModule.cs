using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace CourseSelecting
{
    [DependsOn(typeof(CourseSelectingCoreModule), typeof(AbpAutoMapperModule))]
    public class CourseSelectingApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            AutoMapperWebConfig.Configure();//一次性加载所有映射配置
        }
    }
}
