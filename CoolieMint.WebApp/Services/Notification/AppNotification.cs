using System;

namespace CoolieMint.WebApp.Services.Notification
{
    public class AppNotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Uri Uri { get; set; }
        public string UriName { get; set; }
    }
}
