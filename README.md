# Municipal Services Application - POE Part 1

## Overview
This is a C# .NET Framework Windows Forms application designed to streamline municipal services in South Africa. The application enables citizens to report issues and request various municipal services with an integrated push notification system for enhanced user engagement.

## User Engagement Strategy: Push Notifications
This application implements **push notifications** as the primary user engagement strategy. The system:
- Sends timely reminders to keep users engaged with municipal services
- Provides status updates on submitted issues
- Delivers encouraging messages to promote active citizenship
- Builds trust through regular communication with citizens

## Features Implemented

### ✅ Main Menu
- **Report Issues**: Fully functional issue reporting system
- **Local Events and Announcements**: Coming soon (disabled)
- **Service Request Status**: Coming soon (disabled)

### ✅ Report Issues Functionality
- **Location Input**: Text field for specifying issue location
- **Category Selection**: Dropdown with predefined categories (Sanitation, Roads, Utilities, Public Safety, Parks and Recreation, Other)
- **Description Box**: Rich text area for detailed issue description
- **Media Attachment**: File browser for attaching images and documents
- **Progress Tracking**: Visual progress bar during submission
- **Push Notifications**: Automatic notifications for submission confirmation and status updates

### ✅ Push Notification System
- Welcome notifications when app starts
- Issue submission confirmations
- Status update notifications
- Periodic engagement reminders (every 30 seconds for demo)
- Encouraging messages to promote community participation

### ✅ Data Management
- Uses `List<Issue>` data structure for efficient issue storage
- Issue model with properties: Id, Location, Category, Description, DateReported, Status, AttachedFiles
- Static data persistence during application session

## Technical Requirements Met
- ✅ Appropriate data structures (List for storing issues)
- ✅ User-friendly interface with consistent design
- ✅ Form validation and error handling
- ✅ Event handling for all interactive elements
- ✅ Responsive layout design
- ✅ Clear navigation between forms

## How to Compile and Run

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- Windows operating system

### Compilation Steps
1. Open Visual Studio
2. Create a new Windows Forms App (.NET Framework) project
3. Copy all the provided source files to your project directory:
   - `Program.cs`
   - `MainForm.cs`
   - `ReportIssuesForm.cs`
   - `Models/Issue.cs`
   - `Services/NotificationService.cs`
4. Add references if needed (System.Windows.Forms should be included by default)
5. Build the solution (Ctrl+Shift+B)
6. Run the application (F5 or Ctrl+F5)

### Alternative Compilation (Command Line)
```bash
# Navigate to project directory
cd "C:\Users\pc\Desktop\Prog7312POE"

# Compile using csc (C# compiler)
csc /target:winexe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll *.cs Models\*.cs Services\*.cs
```

## Usage Instructions

### Starting the Application
1. Launch the application
2. You'll see a welcome notification demonstrating the push notification feature
3. The main menu displays three options with only "Report Issues" enabled

### Reporting an Issue
1. Click "Report Issues" from the main menu
2. Fill in the required fields:
   - **Location**: Enter the specific location of the issue
   - **Category**: Select from the dropdown menu
   - **Description**: Provide detailed information about the issue
3. Optionally attach media files (images, documents)
4. Click "Submit Report"
5. Watch the progress bar and receive push notifications
6. The system will assign an issue ID and confirm submission

### Push Notifications
- **Welcome Message**: Appears when starting the app
- **Submission Confirmation**: Shows when an issue is successfully submitted
- **Status Updates**: Simulated status change notifications
- **Engagement Reminders**: Periodic messages encouraging civic participation

## Project Structure
```
Prog7312POE/
├── Program.cs                 # Application entry point
├── MainForm.cs               # Main menu interface
├── ReportIssuesForm.cs       # Issue reporting form
├── Models/
│   └── Issue.cs              # Issue data model
├── Services/
│   └── NotificationService.cs # Push notification system
└── README.md                 # This file
```

## Design Considerations

### Consistency
- Uniform color scheme (blue tones with green accents)
- Consistent font usage (Segoe UI)
- Standardized button styling and spacing

### Clarity
- Clear labels and instructions
- Intuitive navigation
- Visual feedback for user actions

### User Feedback
- Progress indicators during submission
- Success/error messages
- Push notifications for engagement

### Responsiveness
- Fixed form sizes for consistent experience
- Proper control alignment and spacing

## Push Notification Strategy Benefits
Based on research by Hart et al. (2019, 2020), this implementation provides:
- **Improved Engagement**: Regular reminders keep citizens connected
- **Trust Building**: Timely updates show municipal responsiveness
- **Cost-Effective**: Automated notifications require minimal resources
- **User-Centric**: Brings information directly to users without requiring constant app checking

## Future Enhancements (Part 2)
- Local Events and Announcements functionality
- Service Request Status tracking
- Database integration
- Advanced notification customization
- Mobile responsiveness improvements

## Author
Developed as part of PROG7312 POE Part 1 - Municipal Services Application with Push Notification Strategy

## References
Hart, T. G. B. et al. (2019) 'Innovation for Development in South Africa: Experiences with Basic Service Technologies in Distressed Municipalities', Forum for Development Studies, vol. 47, no. 1, pp. 23-47.

Hart, et al. (2020) Innovation for development in South Africa experiences with basic service technologies.
