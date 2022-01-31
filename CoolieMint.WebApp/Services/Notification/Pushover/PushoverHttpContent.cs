using System;

namespace CoolieMint.WebApp.Services.Notification.Pushover
{
    public class PushoverHttpContent
    {
        public string user { get; set; }
        public string token { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public string url_title { get; set; }
        public int priority { get; set; }
        public string timestamp { get; set; }
        public string sound { get; set; }
        public string attachment { get; set; }
    }
}
