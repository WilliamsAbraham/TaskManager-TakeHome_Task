using Application.Interfaces;
using Domain.Entities;
using infrastructure;
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
        

        public NoticeBackGroundService(ITaskService _taskService,  ApplicationContext _context)
        {
            taskService = _taskService;
            
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
                var notice = new Notification();
                notice.Message = $"{task.Title} by {task.User.UserName} will be due within 48 hours";
                notice.Type = NotificationType.DuedateReminder;
                notice.IsRead = false;

               await context.Notifications.AddAsync(notice);
                context.SaveChanges();
            }
            
            
        }
    }
}
