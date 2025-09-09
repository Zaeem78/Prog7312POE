using System;

namespace MunicipalServicesApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }

        public Notification()
        {
            Timestamp = DateTime.Now;
            IsRead = false;
        }
    }

    public enum NotificationType
    {
        Welcome,
        IssueSubmitted,
        StatusUpdate,
        Engagement,
        System
    }
}
