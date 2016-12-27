using Abp.Web.Mvc.Views;

namespace CourseSelecting.Web.Views
{
    public abstract class CourseSelectingWebViewPageBase : CourseSelectingWebViewPageBase<dynamic>
    {

    }

    public abstract class CourseSelectingWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CourseSelectingWebViewPageBase()
        {
            LocalizationSourceName = CourseSelectingConsts.LocalizationSourceName;
        }
    }
}