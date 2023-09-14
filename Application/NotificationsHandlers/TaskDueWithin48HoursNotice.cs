﻿using Application.Services.NotificationService;
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
    public record TaskDueWithin48HoursNotice(MyTask Task):INotification;
    
    

    public class NotificehHandler : INotificationHandler<TaskDueWithin48HoursNotice>
    {
        private readonly NotificationRepository notificationRepository;
        public NotificehHandler(NotificationRepository _notificationRepository)
        {
            this.notificationRepository = _notificationRepository;
                
        }

        public async Task Handle(TaskDueWithin48HoursNotice notification, CancellationToken cancellationToken)
        {
            var notice = new Notification
            {
                Message = $"{notification.Task.Title} by {notification.Task.User.UserName} will be due within 48 hours",
                Type = NotificationType.DuedateReminder,
                IsRead = false,
                User = notification.Task.User
            };
            await notificationRepository.CreateNotification(notice);
        
        }
    }
}
