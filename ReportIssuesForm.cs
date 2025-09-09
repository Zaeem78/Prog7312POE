using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        private TextBox txtLocation;
        private ComboBox cmbCategory;
        private RichTextBox txtDescription;
        private Button btnAttachMedia;
        private Button btnSubmit;
        private Button btnBackToMain;
        private Label lblProgress;
        private ProgressBar progressBar;
        private ListBox lstAttachedFiles;
        
        private static List<Issue> reportedIssues = new List<Issue>();
        private static int nextIssueId = 1;
        private List<string> attachedFiles;
        private Button btnNotifications;
        private Label lblNotificationCount;
        private NotificationService notificationService;

        public ReportIssuesForm()
        {
            InitializeComponent();
            attachedFiles = new List<string>();
            notificationService = NotificationService.Instance;
            notificationService.NotificationAdded += OnNotificationAdded;
            UpdateNotificationCount();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Report Issues - Municipal Services";
            this.Size = new Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title
            Label lblTitle = new Label();
            lblTitle.Text = "Report Municipal Issues";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(25, 25, 112);
            lblTitle.Size = new Size(400, 30);
            lblTitle.Location = new Point(150, 20);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Location Label and TextBox
            Label lblLocation = new Label();
            lblLocation.Text = "Location:";
            lblLocation.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblLocation.Size = new Size(100, 25);
            lblLocation.Location = new Point(50, 70);

            txtLocation = new TextBox();
            txtLocation.Font = new Font("Segoe UI", 10);
            txtLocation.Size = new Size(500, 25);
            txtLocation.Location = new Point(50, 95);
            txtLocation.Text = "Enter the location of the issue (e.g., Corner of Main St and Oak Ave)";
            txtLocation.ForeColor = Color.Gray;
            txtLocation.Enter += TxtLocation_Enter;
            txtLocation.Leave += TxtLocation_Leave;

            // Category Label and ComboBox
            Label lblCategory = new Label();
            lblCategory.Text = "Category:";
            lblCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCategory.Size = new Size(100, 25);
            lblCategory.Location = new Point(50, 130);

            cmbCategory = new ComboBox();
            cmbCategory.Font = new Font("Segoe UI", 10);
            cmbCategory.Size = new Size(500, 25);
            cmbCategory.Location = new Point(50, 155);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new string[] {
                "Sanitation", "Roads", "Utilities", "Public Safety", "Parks and Recreation", "Other"
            });

            // Description Label and RichTextBox
            Label lblDescription = new Label();
            lblDescription.Text = "Description:";
            lblDescription.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblDescription.Size = new Size(100, 25);
            lblDescription.Location = new Point(50, 190);

            txtDescription = new RichTextBox();
            txtDescription.Font = new Font("Segoe UI", 10);
            txtDescription.Size = new Size(500, 100);
            txtDescription.Location = new Point(50, 215);

            // Media Attachment
            btnAttachMedia = new Button();
            btnAttachMedia.Text = "📎 Attach Media";
            btnAttachMedia.Font = new Font("Segoe UI", 10);
            btnAttachMedia.Size = new Size(150, 35);
            btnAttachMedia.Location = new Point(50, 330);
            btnAttachMedia.BackColor = Color.FromArgb(70, 130, 180);
            btnAttachMedia.ForeColor = Color.White;
            btnAttachMedia.FlatStyle = FlatStyle.Flat;
            btnAttachMedia.FlatAppearance.BorderSize = 0;
            btnAttachMedia.Cursor = Cursors.Hand;
            btnAttachMedia.Click += BtnAttachMedia_Click;

            // Attached Files ListBox
            lstAttachedFiles = new ListBox();
            lstAttachedFiles.Font = new Font("Segoe UI", 9);
            lstAttachedFiles.Size = new Size(330, 60);
            lstAttachedFiles.Location = new Point(220, 330);

            // Progress Bar and Label
            progressBar = new ProgressBar();
            progressBar.Size = new Size(500, 20);
            progressBar.Location = new Point(50, 410);
            progressBar.Visible = false;

            lblProgress = new Label();
            lblProgress.Text = "Ready to submit your report";
            lblProgress.Font = new Font("Segoe UI", 9);
            lblProgress.Size = new Size(500, 20);
            lblProgress.Location = new Point(50, 435);
            lblProgress.ForeColor = Color.FromArgb(34, 139, 34);

            // Submit Button
            btnSubmit = new Button();
            btnSubmit.Text = "Submit Report";
            btnSubmit.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnSubmit.Size = new Size(150, 40);
            btnSubmit.Location = new Point(300, 470);
            btnSubmit.BackColor = Color.FromArgb(34, 139, 34);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Cursor = Cursors.Hand;
            btnSubmit.Click += BtnSubmit_Click;

            // Back to Main Button
            btnBackToMain = new Button();
            btnBackToMain.Text = "← Back to Main";
            btnBackToMain.Font = new Font("Segoe UI", 10);
            btnBackToMain.Size = new Size(120, 40);
            btnBackToMain.Location = new Point(50, 470);
            btnBackToMain.BackColor = Color.FromArgb(128, 128, 128);
            btnBackToMain.ForeColor = Color.White;
            btnBackToMain.FlatStyle = FlatStyle.Flat;
            btnBackToMain.FlatAppearance.BorderSize = 0;
            btnBackToMain.Cursor = Cursors.Hand;
            btnBackToMain.Click += BtnBackToMain_Click;

            // Notification Bell Button
            btnNotifications = new Button();
            btnNotifications.Text = "🔔";
            btnNotifications.Font = new Font("Segoe UI", 16);
            btnNotifications.Size = new Size(50, 50);
            btnNotifications.Location = new Point(600, 20);
            btnNotifications.BackColor = Color.FromArgb(70, 130, 180);
            btnNotifications.ForeColor = Color.White;
            btnNotifications.FlatStyle = FlatStyle.Flat;
            btnNotifications.FlatAppearance.BorderSize = 0;
            btnNotifications.Cursor = Cursors.Hand;
            btnNotifications.Click += BtnNotifications_Click;

            // Notification Count Label
            lblNotificationCount = new Label();
            lblNotificationCount.BackColor = Color.Red;
            lblNotificationCount.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblNotificationCount.ForeColor = Color.White;
            lblNotificationCount.Size = new Size(20, 20);
            lblNotificationCount.Location = new Point(630, 15);
            lblNotificationCount.Text = "0";
            lblNotificationCount.TextAlign = ContentAlignment.MiddleCenter;
            lblNotificationCount.Visible = false;

            // Add all controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblLocation);
            this.Controls.Add(txtLocation);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(btnAttachMedia);
            this.Controls.Add(lstAttachedFiles);
            this.Controls.Add(progressBar);
            this.Controls.Add(lblProgress);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnBackToMain);
            this.Controls.Add(btnNotifications);
            this.Controls.Add(lblNotificationCount);

            this.ResumeLayout(false);
        }

        private void BtnAttachMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Document Files|*.pdf;*.doc;*.docx;*.txt|All Files|*.*";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select files to attach";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    attachedFiles.Add(fileName);
                    lstAttachedFiles.Items.Add(Path.GetFileName(fileName));
                }
                
                lblProgress.Text = $"{attachedFiles.Count} file(s) attached";
                lblProgress.ForeColor = Color.FromArgb(70, 130, 180);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                ShowSubmissionProgress();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text) || txtLocation.Text == "Enter the location of the issue (e.g., Corner of Main St and Oak Ave)")
            {
                MessageBox.Show("Please enter a location for the issue.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLocation.Focus();
                return false;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category for the issue.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please provide a description of the issue.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void ShowSubmissionProgress()
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            btnSubmit.Enabled = false;
            
            Timer progressTimer = new Timer();
            progressTimer.Interval = 100;
            progressTimer.Tick += (s, e) =>
            {
                progressBar.Value += 5;
                
                if (progressBar.Value <= 30)
                    lblProgress.Text = "Validating submission...";
                else if (progressBar.Value <= 60)
                    lblProgress.Text = "Processing attachments...";
                else if (progressBar.Value <= 90)
                    lblProgress.Text = "Submitting to municipal database...";
                else if (progressBar.Value >= 100)
                {
                    progressTimer.Stop();
                    SubmitIssue();
                }
            };
            progressTimer.Start();
        }

        private void SubmitIssue()
        {
            Issue newIssue = new Issue
            {
                Id = nextIssueId++,
                Location = txtLocation.Text,
                Category = cmbCategory.SelectedItem.ToString(),
                Description = txtDescription.Text,
                AttachedFiles = new List<string>(attachedFiles)
            };

            reportedIssues.Add(newIssue);
            
            lblProgress.Text = $"✅ Issue #{newIssue.Id} submitted successfully!";
            lblProgress.ForeColor = Color.FromArgb(34, 139, 34);
            
            // Show push notification
            notificationService.ShowIssueSubmittedNotification(newIssue.Id);
            
            // Simulate status update notification after a delay
            Timer statusTimer = new Timer();
            statusTimer.Interval = 5000; // 5 seconds
            statusTimer.Tick += (s, e) =>
            {
                statusTimer.Stop();
                notificationService.ShowStatusUpdateNotification(newIssue.Id, "Under Review");
            };
            statusTimer.Start();

            ClearForm();
        }

        private void ClearForm()
        {
            txtLocation.Text = "Enter the location of the issue (e.g., Corner of Main St and Oak Ave)";
            txtLocation.ForeColor = Color.Gray;
            cmbCategory.SelectedIndex = -1;
            txtDescription.Clear();
            attachedFiles.Clear();
            lstAttachedFiles.Items.Clear();
            progressBar.Visible = false;
            btnSubmit.Enabled = true;
        }

        private void TxtLocation_Enter(object sender, EventArgs e)
        {
            if (txtLocation.Text == "Enter the location of the issue (e.g., Corner of Main St and Oak Ave)")
            {
                txtLocation.Text = "";
                txtLocation.ForeColor = Color.Black;
            }
        }

        private void TxtLocation_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                txtLocation.Text = "Enter the location of the issue (e.g., Corner of Main St and Oak Ave)";
                txtLocation.ForeColor = Color.Gray;
            }
        }

        private void BtnBackToMain_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public static List<Issue> GetReportedIssues()
        {
            return new List<Issue>(reportedIssues);
        }
    }
}
