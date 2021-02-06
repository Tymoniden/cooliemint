using System;
using System.Collections.Generic;
using System.Linq;
using WebControlCenter.Database.Models;
using WebControlCenter.Database.Services;

namespace WebControlCenter.Services.Database
{
    public class NotificationService : INotificationService
    {
        private readonly IModelFactory _modelFactory;
        List<Notification> _notifications = new List<Notification>();

        public NotificationService(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(Exception exception, DateTime dateTime)
        {
            _notifications.Add(_modelFactory.CreateNotification(exception, dateTime));
        }

        public void AddNotification(Exception exception, string message, DateTime dateTime)
        {
            _notifications.Add(_modelFactory.CreateNotification(exception, message, dateTime));
        }

        public void RemoveNotification(long id)
        {
            var notification = _notifications.FirstOrDefault(n => n.Id == id);
            if(notification != null)
            {
                _notifications.Remove(notification);
            }
        }

        public void RemoveAllNotifications()
        {
            _notifications.Clear();
        }

        public List<Notification> GetSystemNotifications()
        {
            return _notifications;
        }
    }
}
