﻿using Application.Handlers;
using Application.Interfaces;
using Application.Services.NotificationService;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.NotificationEnums;

namespace Application.Notifications
{
    public record TaskCompleteNotice(MyTask Task):INotification;


    public class TaskCompleteNoticNoticeHandler : INotificationHandler<TaskCompleteNotice>
    {
        private readonly NotificationRepository notificationRepository;
        private readonly IMediator mediator;
        public TaskCompleteNoticNoticeHandler(NotificationRepository _notificationRepo, IMediator mediator)
        {
            notificationRepository = _notificationRepo;
            this.mediator = mediator;
        }

        public async Task Handle(TaskCompleteNotice notification, CancellationToken cancellationToken)
        {
            var notice = new Notification
            {
                Message = $"{notification.Task.Title} assigned to {notification.Task.User.UserName} has been completed",
                Type = NotificationType.DuedateReminder,
                IsRead = false,
                User = notification.Task.User,
                To = notification.Task.User.Email
            };
            await notificationRepository.CreateNotification(notice);
            await mediator.Publish(new EmailRequest(notice),cancellationToken);

        }
    }
}
