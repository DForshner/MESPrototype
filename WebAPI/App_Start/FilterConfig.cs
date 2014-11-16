using Core.Web.Concurrency;
using System.Web;
using System.Web.Mvc;

namespace WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ConcurrencyAwareFilterAttribute());
            filters.Add(new ConcurrencyExceptionFilterAttribute());
        }
    }
}
