using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public class PushoverHttpClient
    {
        private readonly HttpClient _httpClient;

        public PushoverHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.pushover.net/1/messages.json");

            httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "cooliemint-webapp");
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage httpRequest)
        {
            return await _httpClient.SendAsync(httpRequest);
        }
    }
}
