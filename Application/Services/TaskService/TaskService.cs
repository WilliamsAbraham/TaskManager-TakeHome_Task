using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Notifications;
using Domain.Entities;
using Domain.StaticObjects;
using infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationContext context;
        private readonly IMediator mediator;
        public TaskService(ApplicationContext applicationContext,IMediator _mediator)
        {
            this.context = applicationContext;
            mediator = _mediator;
        }

        public async Task<MyTask> AddOrRemoveTaskFromProject(Guid projectId, Guid taskId)
        {
            //check if project and task are not null
            var myTask = await context.MyTasks.FindAsync(taskId)
            ?? throw new NotFoundException($"the task with id {taskId} was not found");

            var project = await context.Projects.FindAsync(projectId)
            ?? throw new NotFoundException($"the project with id {projectId} was not found");
            
            myTask.ProjectId = project.Id;
            context.Update(myTask);
            await context.SaveChangesAsync();
            return myTask;
        }

        public async Task<MyTask> AssignTaskToUser(Guid userId, Guid taskId)
        {
            var task = await context.MyTasks.FindAsync(taskId)
            ?? throw new NotFoundException($"the task with id {taskId} was not found");

            var user = await context.Users.FindAsync(userId)
            ?? throw new NotFoundException($"the user with id {userId} was not found");
     
                task.UserId = user.Id;
                context.Update(task);
                await context.SaveChangesAsync();
             await mediator.Publish(new AssignedTaskNoticeHandler(task));
                return task;
        }

        public async Task<IEnumerable<MyTask>> GetAllTasksDueThisWeek()
        {
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            return await context.MyTasks.Where(task=>task.DueDate >= startOfWeek && task.DueDate <= endOfWeek).ToListAsync();
        }

        //To be called by an undergrond service
        public async Task<IEnumerable<MyTask>> GetAllTasksDueWithin48Hours()
        {
            var now = DateTime.UtcNow;
            var whenDue = now.AddHours(48);

            return await context.MyTasks.Where(task => task.DueDate > now && task.DueDate <= whenDue).ToListAsync();

        }

        public async Task<IEnumerable<MyTask>> GetTasksBasedOnPriority(PriorityEnums.PriorityType priorityType)
        {
            if(!Enum.TryParse(typeof(PriorityEnums.PriorityType), priorityType.ToString(), out var priority))
            {
              throw new NotFoundException($"the priority type parsed {priority} is invalid");
            }
            var taskBasedOnPriority = await context.MyTasks.Where(t => t.Priority == priorityType).ToListAsync()
            ?? throw new NotFoundException($"No task based on {priority} type was found");

            return taskBasedOnPriority;
        }

        public async Task<MyTask> MarKTaskComplete(Guid taskId)
        {
            var completedTask = await context.MyTasks.Where(t=>t.Id == taskId).FirstOrDefaultAsync()
             ?? throw new NotFoundException($"the task with id {taskId} does not exist");
                completedTask.IsCompleted = true;
                context.Update(completedTask);
               await  context.SaveChangesAsync();

          //publish this event
             await mediator.Publish(new TaskCompleteNoticeHandler(completedTask));
             return completedTask;


        }

        
    }
}
