using Application.Interfaces;
using Domain.Entities;
using Domain.StaticObjects;
using infrastructure;
using Microsoft.EntityFrameworkCore;
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
            //check if project and task are not null
            var myTask = await context.MyTasks.FindAsync(taskId);
            var project = await context.Projects.FindAsync(projectId);

            if (project!=null && myTask != null)
            {
                //check if project exists

                if(project!=null)
                {
                    
                    myTask.ProjectId = projectId;
                     context.Update(myTask);
                    await context.SaveChangesAsync();
                }
                return null;
               
            }
            return myTask;
        }

        public async Task<IEnumerable<MyTask>> GetAllTasksDueWithin48Hours()
        {
            var now = DateTime.UtcNow;
            var whenDue = now.AddHours(48);

            return await context.MyTasks.Where(task =>task.DueDate >now && task.DueDate<=whenDue).ToListAsync();
        }

        public async Task<IEnumerable<MyTask>> GetTasksBasedOnPriority(PriorityEnums.PriorityType priorityType)
        {
            var  taskBasedOnPriority = await context.MyTasks.Where(t=>t.Priority == priorityType).ToListAsync();
            if(!Enum.TryParse(typeof(PriorityEnums.PriorityType), priorityType.ToString(), out var priority))
            {
                return null;
            }

            if(taskBasedOnPriority.Count == 0)
            {
                return null;
            }
            return taskBasedOnPriority;
        }

        public async Task<MyTask> MarKTaskComplete(Guid taskId)
        {
            var completedTask = await context.MyTasks.Where(t=>t.Id == taskId).FirstOrDefaultAsync();
            if(completedTask != null)
            {
                completedTask.IsCompleted = true;
                context.Update(completedTask);
                context.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return completedTask;
        }

        
    }
}
