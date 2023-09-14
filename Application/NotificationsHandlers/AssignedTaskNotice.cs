using Application.Services.NotificationService;
using Application.Services.TaskService;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.NotificationEnums;

namespace Application.Notifications
{
    public record AssignedTaskNotice(MyTask Task) : INotification;
    public class AssignedTaskNoticeHandler : INotificationHandler<AssignedTaskNotice>
    {
        private readonly NotificationRepository notificationRepository;
        public AssignedTaskNoticeHandler(NotificationRepository notification)
        {
            notificationRepository = notification;
        }

        public async Task Handle(AssignedTaskNotice notification, CancellationToken cancellationToken)
        {
            var notice = new Notification
            {
                Message = $"{notification.Task.Title} has been assined to {notification.Task.User.UserName}",
                Type = NotificationType.DuedateReminder,
                IsRead = false,
                User = notification.Task.User,
                To = notification.Task.User.Email
            };
            await notificationRepository.CreateNotification(notice);
        }
    }
}
