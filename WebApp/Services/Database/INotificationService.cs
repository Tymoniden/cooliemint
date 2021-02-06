using System;
using System.Collections.Generic;
using WebControlCenter.Database.Models;

namespace WebControlCenter.Services.Database
{
    public interface INotificationService
    {
        void AddNotification(Notification notification);
        void AddNotification(Exception exception, DateTime dateTime);
        void AddNotification(Exception exception, string message, DateTime dateTime);
        List<Notification> GetSystemNotifications();
        void RemoveAllNotifications();
        void RemoveNotification(long id);
    }
}