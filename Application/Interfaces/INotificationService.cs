using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> MarkNoticAsReadOrUnread(int noticId, bool isRead);
    }
}
