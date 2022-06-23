using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Vavatech.Shopper.WebApi.HealthChecks
{
    public class NbpApiOptions
    {
        public string Uri { get; set; }
        public string Table { get; set; }
        public string Format { get; set; }
    }

    public class NbpApiHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly NbpApiOptions options;

        public NbpApiHealthCheck(IHttpClientFactory httpClientFactory, IOptions<NbpApiOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            string uri = $"{options.Uri}/{options.Table}?format={options.Format}";

            HttpClient client = httpClientFactory.CreateClient();

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {
                return HealthCheckResult.Degraded();
            }
        }
    }
}
