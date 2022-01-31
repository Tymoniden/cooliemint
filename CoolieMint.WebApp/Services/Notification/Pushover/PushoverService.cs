using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public class PushoverService : IPushoverService
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IPushoverHttpRequestFactory _pushoverHttpRequestFactory;
        private readonly PushoverHttpClient _pushoverHttpClient;

        public PushoverService(ISettingsProvider settingsProvider, IPushoverHttpRequestFactory pushoverHttpRequestFactory, PushoverHttpClient pushoverHttpClient)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
            _pushoverHttpRequestFactory = pushoverHttpRequestFactory ?? throw new ArgumentNullException(nameof(pushoverHttpRequestFactory));
            _pushoverHttpClient = pushoverHttpClient ?? throw new ArgumentNullException(nameof(pushoverHttpClient));
        }

        public async Task SendMessage(AppNotification appNotification)
        {
            if (!_settingsProvider.GetSettings().PushOverAccounts.Any())
            {
                return;
            }

            var messages = new List<HttpRequestMessage>();
            foreach (var pushoverAccount in _settingsProvider.GetSettings().PushOverAccounts)
            {
                messages.Add(_pushoverHttpRequestFactory.CreateHttpRequest(appNotification, pushoverAccount.UserKey, pushoverAccount.ApplicationKey));
            }

            if (messages.Count > 0)
            {
                foreach (var message in messages)
                {
                    var resonse = await _pushoverHttpClient.Send(message);
                }
            }
        }
    }
}
