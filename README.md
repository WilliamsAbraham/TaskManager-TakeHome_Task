# ASP.NET 7 Web API Task Management Project

# We use these packages:
- EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- MySql.EntityFramworkCore
- MediatR
- SendGrid 


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

#PROJECT SET-UP
- Clone or download the main branch of the repo
- run dotnet restore(Nuget package manager console) command to restore the projects along all packages(Make sure you're connected to the internet)
- Set-up your data base (MySql or MsSqls Server)
- Update the ConnectionString's DefaultConnect Node in the appsettings file of the presentation layer
- Also update the Sendgrid API KEY Node in the appsettings file of the presentation layer
- Run the Update-database (Nuget package manager console) command to update your database with the project schema
- Rn the project and navigate to https://localhost:{your port}/swagger/index.html 

