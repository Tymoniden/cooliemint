namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public interface IPushoverHttpContentFactory
    {
        PushoverHttpContent CreateHttpContent(AppNotification appNotification, string applicationKey, string userKey);
    }
}