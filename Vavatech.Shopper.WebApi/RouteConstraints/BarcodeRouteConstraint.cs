namespace Vavatech.Shopper.WebApi.RouteConstraints
{
    // https://github.com/sulmar/Sulmar.AspNetCore.Routing.RouteConstraints
    public class BarcodeRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            throw new NotImplementedException();
        }
    }
}
