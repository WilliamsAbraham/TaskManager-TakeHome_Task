using Domain.Entities;
using Presentation.DTOs;

namespace Presentation
{
    /// <summary>
    /// This class is used to mapper dtos to data entities; a library such as Automapper or MapStar would be used a large and complex projects
    /// </summary>
    public static class DtoMappers
    {
        public static MyTask TaskMapper(TaskDto taskDto)
        {
            var myTask = new MyTask();
            myTask.DateCreated = taskDto.DateCreated;
            myTask.DueDate = taskDto.DueDate;
            myTask.Description = taskDto.Description;
            myTask.Description = taskDto.Description;
            myTask.Priority = taskDto.priorityType;
            myTask.IsCompleted = taskDto.IsCompleted;
            myTask.ProjectId = taskDto.ProjectId;
            myTask.UserId = taskDto.UserId;
            myTask.Title = taskDto.Title;
            
            return myTask;
        }

        public static MyProject ProjectMapper(ProjectDto projectDto)
        {
            var project = new MyProject();
            project.Title = projectDto.Title;
            project.DateCreated =DateTime.Now;
            project.UserId = projectDto.UserId;
            project.MyTasks = projectDto.myTasks;
            

            return project;
        }
        public static User UserMapper(UserDto userDto)
        {
            var user = new User();
            user.Name = userDto.UserName;
            user.Email = userDto.Email;

            return user;
        }

        public static Notification NotificationMapper(NotificationDto notificationDto)
        {
            var notification = new Notification();
            notification.Message = notificationDto.Message;
            notification.IsRead = notificationDto.IsRead;
            notification.Type = notificationDto.NotificationType;

            return notification;
        }
    }
}
