using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using MunicipalServicesApp.Models;

namespace MunicipalServicesApp.Services
{
    public class NotificationService
    {
        private static NotificationService _instance;
        private List<Notification> _notifications;
        private Timer _notificationTimer;
        private int _nextNotificationId = 1;

        public event EventHandler NotificationAdded;

        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotificationService();
                return _instance;
            }
        }

        private NotificationService()
        {
            _notifications = new List<Notification>();
            InitializeNotificationTimer();
        }

        private void InitializeNotificationTimer()
        {
            _notificationTimer = new Timer();
            _notificationTimer.Interval = 30000; // 30 seconds for demo purposes
            _notificationTimer.Tick += ShowEngagementNotification;
            _notificationTimer.Start();
        }

        public void ShowNotification(string title, string message)
        {
            AddNotification(title, message, NotificationType.System);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowIssueSubmittedNotification(int issueId)
        {
            string title = "Issue Submitted";
            string message = $"Your issue #{issueId} has been successfully submitted! You will receive updates on its progress.";
            AddNotification(title, message, NotificationType.IssueSubmitted);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowStatusUpdateNotification(int issueId, string newStatus)
        {
            string title = "Status Update";
            string message = $"Update: Issue #{issueId} status changed to '{newStatus}'. Thank you for using our municipal services!";
            AddNotification(title, message, NotificationType.StatusUpdate);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddNotification(string title, string message, NotificationType type)
        {
            var notification = new Notification
            {
                Id = _nextNotificationId++,
                Title = title,
                Message = message,
                Type = type
            };
            
            _notifications.Add(notification);
            NotificationAdded?.Invoke(this, EventArgs.Empty);
        }

        public List<Notification> GetAllNotifications()
        {
            return new List<Notification>(_notifications);
        }

        public int GetUnreadCount()
        {
            return _notifications.FindAll(n => !n.IsRead).Count;
        }

        public void MarkAllAsRead()
        {
            foreach (var notification in _notifications)
            {
                notification.IsRead = true;
            }
            NotificationAdded?.Invoke(this, EventArgs.Empty);
        }

        private void ShowEngagementNotification(object sender, EventArgs e)
        {
            var engagementMessages = new List<string>
            {
                "💡 Have you reported any issues in your area today? Help improve your community!",
                "🏘️ Your voice matters! Report local issues to help make your neighborhood better.",
                "📢 Stay connected with your municipality - check for service updates!",
                "🤝 Together we can build a better community. Report issues when you see them!",
                "⭐ Thank you for being an active citizen! Your reports help improve services."
            };

            var random = new Random();
            var message = engagementMessages[random.Next(engagementMessages.Count)];
            
            // Add to notification list without showing popup
            AddNotification("Municipal Services Reminder", message, NotificationType.Engagement);
        }

        public void StopNotifications()
        {
            _notificationTimer?.Stop();
        }

        public void StartNotifications()
        {
            _notificationTimer?.Start();
        }
    }
}
