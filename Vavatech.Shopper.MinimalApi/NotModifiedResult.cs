namespace Vavatech.Shopper.MinimalApi
{
    public class NotModifiedResult : IResult
    {
        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 304;

            return Task.CompletedTask;
        }
    }


    public static class ResultsExtensions
    {
        public static IResult NotModified(this IResultExtensions resultExtensions)
        {
            return new NotModifiedResult();
        }
    }
}
