﻿using Core.CQRS.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Infrastructure.Web.Concurrency
{
    public class ConcurrencyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            if (actionExecutedContext.Exception is ConcurrencyException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                return;
            }
        }

    }
}