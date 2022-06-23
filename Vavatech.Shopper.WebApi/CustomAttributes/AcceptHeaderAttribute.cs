using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

namespace Vavatech.Shopper.WebApi.CustomAttributes
{
    public class AcceptHeaderAttribute : ActionMethodSelectorAttribute
    {
        private readonly string contentType;

        public AcceptHeaderAttribute(string contentType)
        {
            this.contentType = contentType;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var requestAccept = routeContext.HttpContext.Request.Headers[HeaderNames.Accept];

            if (StringValues.IsNullOrEmpty(requestAccept) || requestAccept == "*/*")
                return true;

            return requestAccept == contentType;
        }
    }
}
