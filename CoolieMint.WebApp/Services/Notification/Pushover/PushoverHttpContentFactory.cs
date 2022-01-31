namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public class PushoverHttpContentFactory : IPushoverHttpContentFactory
    {
        public PushoverHttpContent CreateHttpContent(AppNotification appNotification, string applicationKey, string userKey)
        {
            return new PushoverHttpContent
            {
                token = applicationKey,
                user = userKey,
                title = appNotification.Title,
                message = appNotification.Message,
                url = appNotification.Uri.ToString(),
                url_title = appNotification.UriName
            };
        }
    }
}
