﻿using Core.CQRS;
using Core.CQRS.Exceptions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Infrastructure.Web.Concurrency
{
    /// <summary>
    /// Forces PUT http method calls to have an ETAG value that matches the associated command's version property.
    /// </summary>
    public class UpdateVersionMustMatchETagFilterAttribute : ActionFilterAttribute
    {
        public UpdateVersionMustMatchETagFilterAttribute()
            : this(0, false)
        {

        }

        public UpdateVersionMustMatchETagFilterAttribute(int maxAgeInSeconds, bool isPublic)
        {
            _isPublic = isPublic;
            _maxAgeInSeconds = maxAgeInSeconds;
        }

 

        private int _maxAgeInSeconds;
        private bool _isPublic;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Debug.Assert(actionContext.ActionArguments.Values.Count(x => x is IVersioned) <= 1, "Expected single command per HTTP request.");

            // Only applies to PUT methods
            if (actionContext.Request.Method.Method != HttpMethod.Put.Method)
            {
                return;
            }

            // only for PUT where if-match header contains a value
            if (actionContext.Request.Headers.IfMatch == null || actionContext.Request.Headers.IfMatch.Count == 0)
            {
                return;
            }

            foreach (var arg in actionContext.ActionArguments.Values)
            {
                var versionedCommand = arg as IVersioned;
                if (versionedCommand != null)
                {
                    ThrowIfETagDoesNotMatchCommandVersion(actionContext, versionedCommand);
                }
            }

            base.OnActionExecuting(actionContext);
        }

        private static void ThrowIfETagDoesNotMatchCommandVersion(HttpActionContext actionContext, IVersioned versionedCommand)
        {
            try
            {
                var decrypted = ETagEncryption.Decrypt(actionContext.Request.Headers.IfMatch.First().Tag.Trim('"')
                    , actionContext.Request.Headers.AcceptEncoding.ToString()); // this is the VARY header

                var ver = -1;
                if (Int32.TryParse(decrypted, out ver))
                {
                    if (versionedCommand.Version != ver)
                        throw new ConcurrencyException("Etag does not match command version.");
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }
    }
}
