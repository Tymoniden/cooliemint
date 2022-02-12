using CoolieMint.WebApp.Services.Http;
using CoolieMint.WebApp.Services.Notification;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Text;
using WebControlCenter.Services;
using static System.Net.Mime.MediaTypeNames;

namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public class PushoverHttpRequestFactory : IPushoverHttpRequestFactory
    {
        readonly Uri _serverUri = new Uri("https://api.pushover.net/1/messages.json");
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IPushoverHttpContentFactory _pushoverHttpContentFactory;
        private readonly IHttpContentFactory _httpContentFactory;

        public PushoverHttpRequestFactory(IJsonSerializerService jsonSerializerService, IPushoverHttpContentFactory pushoverHttpContentFactory, IHttpContentFactory httpContentFactory)
        {
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _pushoverHttpContentFactory = pushoverHttpContentFactory ?? throw new ArgumentNullException(nameof(pushoverHttpContentFactory));
            _httpContentFactory = httpContentFactory ?? throw new ArgumentNullException(nameof(httpContentFactory));
        }

        public HttpRequestMessage CreateHttpRequest(AppNotification appNotification, string userKey, string applicationKey)
        {
            try
            {
                var message = new HttpRequestMessage(HttpMethod.Post, _serverUri);

                var pushoverHttpContent = _pushoverHttpContentFactory.CreateHttpContent(appNotification, applicationKey, userKey);
                var jsonContent = _jsonSerializerService.Serialize(pushoverHttpContent);
                message.Content = _httpContentFactory.CreateJsonStringContent(jsonContent);

                return message;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }


}
