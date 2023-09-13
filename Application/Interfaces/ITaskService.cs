using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.PriorityEnums;

namespace Application.Interfaces
{
    public interface ITaskService
    {

        Task<IEnumerable<MyTask>> GetAllTasksDueWithin48Hours();
        Task<IEnumerable<MyTask>> GetTasksBasedOnPriority(PriorityType priorityType);
        Task<MyTask> AddOrRemoveTaskFromProject(Guid projectId, Guid taskId);
        Task<MyTask> MarKTaskComplete(Guid taskId);
        Task<MyTask> AssignTaskToUser(Guid userId, Guid taskId);
    }
}
