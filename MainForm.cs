using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Services;

namespace MunicipalServicesApp
{
    public partial class MainForm : Form
    {
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Label lblTitle;
        private Label lblWelcome;
        private Button btnNotifications;
        private Label lblNotificationCount;
        private NotificationService notificationService;

        public MainForm()
        {
            InitializeComponent();
            notificationService = NotificationService.Instance;
            notificationService.NotificationAdded += OnNotificationAdded;
            
            // Show welcome notification
            notificationService.ShowNotification("Welcome!", 
                "Welcome to Municipal Services App! Stay connected with push notifications for updates.");
            
            UpdateNotificationCount();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnServiceStatus = new System.Windows.Forms.Button();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.lblNotificationCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.lblTitle.Location = new System.Drawing.Point(161, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 52);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Municipal Services Portal";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblWelcome.Location = new System.Drawing.Point(112, 61);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(500, 66);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Choose a service to get started.";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // btnReportIssues
            // 
            this.btnReportIssues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.btnReportIssues.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportIssues.FlatAppearance.BorderSize = 0;
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReportIssues.ForeColor = System.Drawing.Color.White;
            this.btnReportIssues.Location = new System.Drawing.Point(259, 130);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(200, 71);
            this.btnReportIssues.TabIndex = 2;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = false;
            this.btnReportIssues.Click += new System.EventHandler(this.BtnReportIssues_Click);
            // 
            // btnLocalEvents
            // 
            this.btnLocalEvents.BackColor = System.Drawing.Color.Gray;
            this.btnLocalEvents.Enabled = false;
            this.btnLocalEvents.FlatAppearance.BorderSize = 0;
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLocalEvents.ForeColor = System.Drawing.Color.White;
            this.btnLocalEvents.Location = new System.Drawing.Point(259, 207);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(200, 71);
            this.btnLocalEvents.TabIndex = 3;
            this.btnLocalEvents.Text = "Local Events and\nAnnouncements\n(Coming Soon)";
            this.btnLocalEvents.UseVisualStyleBackColor = false;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);
            // 
            // btnServiceStatus
            // 
            this.btnServiceStatus.BackColor = System.Drawing.Color.Gray;
            this.btnServiceStatus.Enabled = false;
            this.btnServiceStatus.FlatAppearance.BorderSize = 0;
            this.btnServiceStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServiceStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnServiceStatus.ForeColor = System.Drawing.Color.White;
            this.btnServiceStatus.Location = new System.Drawing.Point(259, 284);
            this.btnServiceStatus.Name = "btnServiceStatus";
            this.btnServiceStatus.Size = new System.Drawing.Size(200, 71);
            this.btnServiceStatus.TabIndex = 4;
            this.btnServiceStatus.Text = "Service Request Status\n(Coming Soon)";
            this.btnServiceStatus.UseVisualStyleBackColor = false;
            // 
            // btnNotifications
            // 
            this.btnNotifications.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(130)))), ((int)(((byte)(180)))));
            this.btnNotifications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotifications.FlatAppearance.BorderSize = 0;
            this.btnNotifications.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifications.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.btnNotifications.ForeColor = System.Drawing.Color.White;
            this.btnNotifications.Location = new System.Drawing.Point(650, 20);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(50, 50);
            this.btnNotifications.TabIndex = 5;
            this.btnNotifications.Text = "🔔";
            this.btnNotifications.UseVisualStyleBackColor = false;
            this.btnNotifications.Click += new System.EventHandler(this.BtnNotifications_Click);
            // 
            // lblNotificationCount
            // 
            this.lblNotificationCount.BackColor = System.Drawing.Color.Red;
            this.lblNotificationCount.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblNotificationCount.ForeColor = System.Drawing.Color.White;
            this.lblNotificationCount.Location = new System.Drawing.Point(680, 15);
            this.lblNotificationCount.Name = "lblNotificationCount";
            this.lblNotificationCount.Size = new System.Drawing.Size(20, 20);
            this.lblNotificationCount.TabIndex = 6;
            this.lblNotificationCount.Text = "0";
            this.lblNotificationCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotificationCount.Visible = false;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(736, 446);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnReportIssues);
            this.Controls.Add(this.btnLocalEvents);
            this.Controls.Add(this.btnServiceStatus);
            this.Controls.Add(this.btnNotifications);
            this.Controls.Add(this.lblNotificationCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Application";
            this.ResumeLayout(false);

        }

        private void BtnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm();
            reportForm.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            notificationService.StopNotifications();
            base.OnFormClosing(e);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void BtnNotifications_Click(object sender, EventArgs e)
        {
            NotificationPanel notificationPanel = new NotificationPanel();
            notificationPanel.ShowDialog();
            UpdateNotificationCount();
        }

        private void OnNotificationAdded(object sender, EventArgs e)
        {
            UpdateNotificationCount();
        }

        private void UpdateNotificationCount()
        {
            int unreadCount = notificationService.GetUnreadCount();
            if (unreadCount > 0)
            {
                lblNotificationCount.Text = unreadCount.ToString();
                lblNotificationCount.Visible = true;
            }
            else
            {
                lblNotificationCount.Visible = false;
            }
        }
    }
}
