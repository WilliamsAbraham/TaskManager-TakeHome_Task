using Application.Interfaces;
using Domain.Entities;
using infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationContext context;
        public NotificationService(ApplicationContext _context) 
        {
            this.context = _context;
        }
        public async Task<Notification> MarkNoticAsReadOrUnread(Guid noticId)
        {
            //Get noticification
            var Notice = await context.Notifications.Where(n=>n.NotificationId == noticId).FirstOrDefaultAsync();
            if (Notice == null)
            {
                //throw exception if notification is not found
                throw new Exception("Notification entity not found");
            }
            //if marked unread, mark read
            if(Notice.IsRead ==false)
            {
                Notice.IsRead = true;
            }
            //if marked unread, mark read
            else if(Notice.IsRead ==true)
            {
                Notice.IsRead = false;
            }

            context.Update(Notice);
            context.SaveChanges();
            return Notice;
        }
    }
}
