using Application.Interfaces;
using Domain.Entities;
using Domain.StaticObjects;
using infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationContext context;
        public TaskService(ApplicationContext applicationContext)
        {
            this.context = applicationContext;
            
        }

        public async Task<MyTask> AddOrRemoveTaskFromProject(Guid projectId, int taskId)
        {
            //check if projectId is not null
            var myTask = await context.MyTasks.FindAsync(taskId);
            if (projectId!=null && myTask != null)
            {
                //check if project exists

                var project = await context.Projects.FindAsync(projectId);
                if(project!=null)
                {
                    
                    myTask.ProjectId = projectId;
                }

               
            }
        }

        public Task<IEnumerable<MyTask>> GetAllTasksDueWithin48Hours()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MyTask>> GetTasksBasedOnPriority(PriorityEnums.PriorityType priorityType)
        {
            throw new NotImplementedException();
        }

       
    }
}
