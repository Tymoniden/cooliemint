using System;

namespace WebControlCenter.Database.Models
{
    public class Notification
    {
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public NotificationSeverity NotificationSeverity { get; set; }

        public string Message { get; set; }
    }
}
