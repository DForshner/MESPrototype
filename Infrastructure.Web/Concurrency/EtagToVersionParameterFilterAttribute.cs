using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Infrastructure.Web.Concurrency
{
    public class EtagToVersionParameterFilterAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!actionContext.ActionArguments.ContainsKey("Version"))
                return;


        }
    }
}
