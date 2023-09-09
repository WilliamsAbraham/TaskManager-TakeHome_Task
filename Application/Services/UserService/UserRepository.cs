using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserRepository
    {
        private readonly IRepository<User> repository;

        public UserRepository(IRepository<User> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<User>> GetAllTasks()
        {
            return await repository.GetAllAsync();
        }

        public async Task<User> GetTaskById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateTask(User user)
        {
            repository.AddAsync(user);
        }

        public async Task UpdateTask(User user)
        {
            repository.UpdateAsync(user);
        }
        public async Task DeleteTask(User user)
        {
            repository.DeleteAsync(user);
        }
    }
}
