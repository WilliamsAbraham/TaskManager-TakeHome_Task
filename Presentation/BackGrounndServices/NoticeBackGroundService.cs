using Application.Interfaces;
using Application.Notifications;
using Domain.Entities;
using infrastructure;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.NotificationEnums;

namespace Presentation.BackGrounndServices
{
    public class NoticeBackGroundService : BackgroundService
    {
        private readonly ITaskService taskService;
        private readonly ApplicationContext context;
        private readonly IMediator mediator;
        

        public NoticeBackGroundService(ITaskService _taskService,  ApplicationContext _context,IMediator _mediator)
        {
            taskService = _taskService;
            mediator = _mediator;
            context = _context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await CheckTaskUpdate();
           await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }

        private async Task CheckTaskUpdate()
        {
            var taskDueWithin48Hours =await taskService.GetAllTasksDueWithin48Hours();

            foreach (var task in taskDueWithin48Hours)
            {

                await mediator.Publish(new TaskDueWithin48HoursNotice(task));
               // var notice = new Notification();
               // notice.Message = $"{task.Title} by {task.User.UserName} will be due within 48 hours";
               // notice.Type = NotificationType.DuedateReminder;
               // notice.IsRead = false;

               //await context.Notifications.AddAsync(notice);
               // context.SaveChanges();
            }
            
            
        }
    }
}
