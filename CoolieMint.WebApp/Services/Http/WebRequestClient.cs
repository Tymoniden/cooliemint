using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Http
{
    public class WebRequestClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebRequestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            httpRequestMessage.Headers.Add("Accept", "application/vnd.github.v3+json");
            httpRequestMessage.Headers.Add("User-Agent", "HttpClientFactory-Sample");


            var client = _httpClientFactory.CreateClient();

            return await client.SendAsync(httpRequestMessage);
        }
    }
}
