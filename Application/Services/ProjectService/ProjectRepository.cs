using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProjectService
{
    //This class uses the generic repository concrete implementation to create a data access logic for tasks entity
    public class ProjectRepository
    {
        private readonly IRepository<Project> repository;

        public ProjectRepository(IRepository<Project> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<Project>> GetAllTasks()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Project> GetTaskById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateTask(Project project)
        {
            repository.AddAsync(project);
        }

        public async Task UpdateTask(Project project)
        {
            repository.UpdateAsync(project);
        }
        public async Task DeleteTask(Project project)
        {
            repository.DeleteAsync(project);
        }
    }
}
