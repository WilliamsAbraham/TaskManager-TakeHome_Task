# ASP.NET 7 Web API Task Management Project

# We use these packages:
- EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- MediatR


## Features of this repository:
PROJECT ENDPOINTS
- CRUD operations for Tasks, Projects, Users, and Notifications.
- An endpoint to fetch tasks based on their status or priority.
- An endpoint to fetch tasks due for the current week.
- An endpoint to assign a task to a project or remove it from a project.
- An endpoint to mark a notification as read or unread.
##
NOTIFICATIONS
users will be notified when:
- A task's due date is within 48 hours.
- A task they created is marked as completed.
- They are assigned a new task.
The notifcations where implemented within a notification handler using MediatR with a background service to handle the notification of tasks due with 48 hours.

