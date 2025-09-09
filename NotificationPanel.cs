using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services;

namespace MunicipalServicesApp
{
    public partial class NotificationPanel : Form
    {
        private NotificationService notificationService;
        private ListView listViewNotifications;
        private Button btnMarkAllRead;
        private Label lblTitle;

        public NotificationPanel()
        {
            InitializeComponent();
            notificationService = NotificationService.Instance;
            LoadNotifications();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Notifications";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            lblTitle = new Label();
            lblTitle.Text = "🔔 Notifications";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(25, 25, 112);
            lblTitle.Size = new Size(200, 30);
            lblTitle.Location = new Point(20, 15);

            // Mark All Read Button
            btnMarkAllRead = new Button();
            btnMarkAllRead.Text = "Mark All as Read";
            btnMarkAllRead.Font = new Font("Segoe UI", 9);
            btnMarkAllRead.Size = new Size(120, 30);
            btnMarkAllRead.Location = new Point(350, 15);
            btnMarkAllRead.BackColor = Color.FromArgb(70, 130, 180);
            btnMarkAllRead.ForeColor = Color.White;
            btnMarkAllRead.FlatStyle = FlatStyle.Flat;
            btnMarkAllRead.FlatAppearance.BorderSize = 0;
            btnMarkAllRead.Cursor = Cursors.Hand;
            btnMarkAllRead.Click += BtnMarkAllRead_Click;

            // Notifications ListView
            listViewNotifications = new ListView();
            listViewNotifications.Location = new Point(20, 60);
            listViewNotifications.Size = new Size(450, 480);
            listViewNotifications.View = View.Details;
            listViewNotifications.FullRowSelect = true;
            listViewNotifications.GridLines = true;
            listViewNotifications.HeaderStyle = ColumnHeaderStyle.None;
            listViewNotifications.Font = new Font("Segoe UI", 9);
            
            // Add columns
            listViewNotifications.Columns.Add("Notification", 450);

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(btnMarkAllRead);
            this.Controls.Add(listViewNotifications);

            this.ResumeLayout(false);
        }

        private void LoadNotifications()
        {
            listViewNotifications.Items.Clear();
            var notifications = notificationService.GetAllNotifications()
                .OrderByDescending(n => n.Timestamp)
                .ToList();

            foreach (var notification in notifications)
            {
                var item = new ListViewItem();
                item.Tag = notification;
                
                // Create notification display text
                string timeAgo = GetTimeAgo(notification.Timestamp);
                string displayText = $"{notification.Title}\n{notification.Message}\n{timeAgo}";
                item.Text = displayText;
                
                // Set colors based on read status
                if (!notification.IsRead)
                {
                    item.BackColor = Color.FromArgb(230, 240, 255);
                    item.ForeColor = Color.FromArgb(25, 25, 112);
                    item.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.FromArgb(70, 70, 70);
                    item.Font = new Font("Segoe UI", 9);
                }

                // Add blue dot for unread notifications
                if (!notification.IsRead)
                {
                    item.Text = "🔵 " + item.Text;
                }

                listViewNotifications.Items.Add(item);
            }

            // Update button state
            btnMarkAllRead.Enabled = notificationService.GetUnreadCount() > 0;
        }

        private string GetTimeAgo(DateTime timestamp)
        {
            var timeSpan = DateTime.Now - timestamp;
            
            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            else if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} min ago";
            else if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hr ago";
            else
                return timestamp.ToString("MMM dd, HH:mm");
        }

        private void BtnMarkAllRead_Click(object sender, EventArgs e)
        {
            notificationService.MarkAllAsRead();
            LoadNotifications();
        }
    }
}
