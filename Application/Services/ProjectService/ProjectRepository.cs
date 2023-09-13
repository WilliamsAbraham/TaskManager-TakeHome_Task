using Application.Common.Exceptions;
using Application.Interfaces;
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
    public class ProjectRepository : IProjectRepo
    {
        private readonly IRepository<MyProject> repository;

        public ProjectRepository(IRepository<MyProject> _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<MyProject>> GetAllProjects(CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync(cancellationToken) 
                ?? throw new NotFoundException($"No Project was found");
        }

        public async Task<MyProject> GetProjectById(Guid id)
        {
            return await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"The Project with id{id} was not found");
        }

        public async Task CreateProject(MyProject project)
        {
           await repository.AddAsync(project);
        }

        public async Task UpdateProject(Guid id,MyProject project)
        {
            var projToUpdate = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"The Project with id{id} was not found");
            projToUpdate.Title = project.Title;
            
           await repository.UpdateAsync(projToUpdate);
        }
        public async Task DeleteProject(Guid id)
        {
            var projToDelete = await repository.GetByIdAsync(id) 
                ?? throw new NotFoundException($"The Project with id{id} was not found");
            await repository.DeleteAsync(projToDelete);
        }

        
    }
}
