using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using infrastructure.Repository;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer t;

        public TaskRepository(IRepository<MyTask> _repository)
        {
                repository = _repository;
           
        }

        public async Task<IEnumerable<MyTask>> GetAllTasks(CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken) 
                ?? throw new NotFoundException(t[$"No Task was found"]);
        }

        public async Task<MyTask> GetTaskById(Guid id)
        {
            return await repository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"The Task with id {id} was not found");
        }

        public async Task CreateTask(MyTask myTask)
        {
           await repository.AddAsync(myTask);
        }

        public async Task UpdateTask(Guid id,MyTask myTask)
        {
            var taskToUpdate = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"The Task with id {id} was not found");
            taskToUpdate.Description = myTask.Description;
            taskToUpdate.Priority = myTask.Priority;
            taskToUpdate.Title = myTask.Title;
            taskToUpdate.Priority = myTask.Priority;
            taskToUpdate.DueDate = myTask.DueDate;
            taskToUpdate.IsCompleted = myTask.IsCompleted;
      
           await repository.UpdateAsync(taskToUpdate); 
        }
        public async Task DeleteTask(Guid id)
        {
            var taskToDelete = await repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"The Task with id {id} was not found");

            await repository.DeleteAsync(taskToDelete);
        }
    }
}
