﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Infrastructure.Web.Concurrency
{
    /// <summary>
    /// Sets GET methods response ETAG based on content's version.
    /// </summary>
    public class ConcurrencyAwareFilterAttribute : ActionFilterAttribute
    {
        private int _maxAgeInSeconds;
        private bool _isPublic;

        public ConcurrencyAwareFilterAttribute()
            : this(0, false)
        { }

        public ConcurrencyAwareFilterAttribute(int maxAgeInSeconds, bool isPublic)
        {
            _isPublic = isPublic;
            _maxAgeInSeconds = maxAgeInSeconds;
        }

        // Adding ETag
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            if (NotGET(actionExecutedContext))
            {
                return;
            }

            if (actionExecutedContext.Response.Content == null)
            {
                return;
            }

            if (ETagExists(actionExecutedContext))
            {
                return;
            }

            var eTag = GenerateETagFromContent(actionExecutedContext);
            if (eTag != null)
            {
                SetEtagAndCacheControlHeader(actionExecutedContext, eTag);
            }
        }

        private static string GenerateETagFromContent(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent == null)
                return null;

            var concurrencyAware = objectContent.Value as IConcurrencyAware;
            if (concurrencyAware == null)
                return null;

            var version = concurrencyAware.ConcurrencyVersion;
            if (version == null)
                return null;

            var encoding = actionExecutedContext.Request.Headers.AcceptEncoding.ToString();
            var eTagString = ETagEncryption.Encrypt(version, encoding);

            return "\"" + eTagString + "\"";
        }

        private static bool ETagExists(HttpActionExecutedContext actionExecutedContext)
        {
            return actionExecutedContext.Response.Headers.ETag != null;
        }

        public bool NotGET(HttpActionExecutedContext actionExecutedContext)
        {
            return (!actionExecutedContext.Request.Method.Method.Equals(HttpMethod.Get.Method)
                && !actionExecutedContext.Request.Method.Method.Equals(HttpMethod.Head.Method));
        }

        private void SetEtagAndCacheControlHeader(HttpActionExecutedContext context, string eTag)
        {
            // return not modified for conditional GET and HEAD
            if (context.Request.Headers.IfNoneMatch != null &&
                context.Request.Headers.IfNoneMatch.Any(etgh =>
                etgh.Tag == eTag))
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.NotModified);
                context.Response.Headers.ETag = new EntityTagHeaderValue(eTag);
                return; // EXIT !!
            }

            context.Response.Headers.ETag = new EntityTagHeaderValue(eTag);
            context.Response.Headers.CacheControl =
                new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromSeconds(_maxAgeInSeconds),
                    Private = !_isPublic
                };
        }
    }
}