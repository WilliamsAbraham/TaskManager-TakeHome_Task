using Application.Interfaces;
using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public class NotificationRepository :INotificationRepo
    {
        private readonly IRepository<Notification> repository;

        public NotificationRepository(IRepository<Notification> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotice(CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken);
        }

        public async Task<Notification> GetNoticeById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateNotification(Notification notification)
        {
            await repository.AddAsync(notification);
        }

        public async Task UpdateNotification(Guid id,Notification notification)
        {
            var noticeToUpdate = await repository.GetByIdAsync(id);
            noticeToUpdate.Message = notification.Message;
            noticeToUpdate.IsRead = notification.IsRead;
            noticeToUpdate.Type = notification.Type;
          await  repository.UpdateAsync(notification);
        }
        public async Task DeleteNotification(Guid id)
        {
            var noticeToDelete = await repository.GetByIdAsync(id);
          await  repository.DeleteAsync(noticeToDelete);
        }
    }
}
