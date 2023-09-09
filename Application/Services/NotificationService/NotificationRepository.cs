using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public class NotificationRepository
    {
        private readonly IRepository<Notification> repository;

        public NotificationRepository(IRepository<Notification> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<Notification>> GetAllTasks()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Notification> GetTaskById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateTask(Notification notification)
        {
            repository.AddAsync(notification);
        }

        public async Task UpdateTask(Notification notification)
        {
            repository.UpdateAsync(notification);
        }
        public async Task DeleteTask(Notification notification)
        {
            repository.DeleteAsync(notification);
        }
    }
}
