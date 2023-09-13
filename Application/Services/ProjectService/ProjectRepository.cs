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
            return await repository.GetAllAsync(cancellationToken);
        }

        public async Task<MyProject> GetProjectById(Guid id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task CreateProject(MyProject project)
        {
           await repository.AddAsync(project);
        }

        public async Task UpdateProject(MyProject project)
        {
           await repository.UpdateAsync(project);
        }
        public async Task DeleteProject(MyProject project)
        {
           await repository.DeleteAsync(project);
        }

        
    }
}
