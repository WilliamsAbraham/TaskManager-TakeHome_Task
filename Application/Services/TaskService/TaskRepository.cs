using Application.Interfaces;
using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TaskService
{
    //This class uses the generic repository concrete implementation to create a data access logic for tasks entity
    public class TaskRepository :ITaskRepo
    {
        private readonly IRepository<MyTask> repository;
        public TaskRepository(IRepository<MyTask> _repository)
        {
                repository = _repository;
        }

        public async Task<IEnumerable<MyTask>> GetAllTasks(CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken);
        }

        public async Task<MyTask> GetTaskById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateTask(MyTask myTask)
        {
           await repository.AddAsync(myTask);
        }

        public async Task UpdateTask(MyTask myTask)
        {
           await repository.UpdateAsync(myTask); 
        }
        public async Task DeleteTask(MyTask myTask)
        {
           await repository.DeleteAsync(myTask);
        }
    }
}
