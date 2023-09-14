using Application.Interfaces;
using Application.Services.NotificationService;
using Application.Services.TaskService;
using AutoMapper;
using Domain.Entities;
using Domain.StaticObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Presentation.DTOs;
using static Domain.StaticObjects.PriorityEnums;


namespace Presentation.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class MyTaskController : ControllerBase
    {
        private readonly TaskRepository taskRepository;
        private readonly ITaskService taskService;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public MyTaskController(TaskRepository _taskRepository,ITaskService _taskService ,IMapper _mapper,IMediator _mediator)
        {
            taskRepository = _taskRepository;
            taskService = _taskService;
            mapper = _mapper;
            mediator = _mediator;
        }

        [HttpGet("/AllTask")]
        public async Task<ActionResult<APIResponse<List<TaskViewModel>>>> GetAllTask(CancellationToken cancellationToken)
        {
            var tasksRetrieved = await taskRepository.GetAllTasks(cancellationToken);
           
            var tasks = mapper.Map<List<TaskViewModel>>(tasksRetrieved);
            return Ok(new APIResponse<List<TaskViewModel>>
            {
                Status = true,
                Data = tasks,
                Message = "Tasks retrived successfully",
            });

        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<APIResponse<TaskViewModel>>> GetTaskById( Guid id)
        {
           try
            {
                var taskRetrieved = await taskRepository.GetTaskById(id);
                var task = mapper.Map<TaskViewModel>(taskRetrieved);
                return Ok(new APIResponse<TaskViewModel>
                {
                    Status = true,
                    Data = task,
                    Message = "Task retrived successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }
        [HttpGet("/All-Task-Due-This-week")]
        public async Task<ActionResult<APIResponse<List<TaskViewModel>>>> GetAllTasksDueThisWeek( )
        {
           try
            {
                var taskRetrieved = await taskService.GetAllTasksDueThisWeek();
                var task = mapper.Map<List<TaskViewModel>>(taskRetrieved);
                return Ok(new APIResponse<List<TaskViewModel>>
                {
                    Status = true,
                    Data = task,
                    Message = "Tasks retrived successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>
                {
                    Status = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("/new-Task")]
        public async Task<ActionResult<APIResponse<string>>> CreateNotification(TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "please correct the errors");
            }
            var task = mapper.Map<MyTask>(taskDto);
            try
            {
                await taskRepository.CreateTask(task);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Task Created Successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message = ex.Message,

                });
            }
        }

        [HttpPut("/EditTask/{id:Guid}")]
        public async Task<ActionResult<APIResponse<string>>> Edit([FromHeader]Guid id, TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "please correct the errors");
            }
            var taskToUpdate = mapper.Map<MyTask>(taskDto);
            try
            {
                await taskRepository.UpdateTask(id, taskToUpdate);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Task Updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("/TaskDeletion/{id:Guid}")]
        public async Task<ActionResult<APIResponse<string>>> Delete([FromHeader]Guid id)
        {
            //var noticeToDelete = mapper.Map<Notification>(notificationDto);

            try
            {
                await taskRepository.DeleteTask(id);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Task deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new APIResponse<object>
                    {
                        Status = false,
                        Data = null,
                        Message = ex.Message,
                    });
            }
        }

        [HttpPost("/project-for-task")]
        public async Task<ActionResult<APIResponse<TaskViewModel>>> RemoveOrAddTaskToProject([FromHeader] Guid taskId,Guid projectId)
        {
            try
            {
                var task = await taskService.AddOrRemoveTaskFromProject(projectId, taskId);
                var taskToReturn = mapper.Map<TaskViewModel>(task);
                return Ok(new APIResponse<TaskViewModel>
                {
                    Status = true,
                    Data = taskToReturn,
                    Message = "Action executed successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message = ex.Message,

                });
            }
        }

        [HttpGet("/Tasks-Based-on-Priority")]
        public async Task<ActionResult<APIResponse<IEnumerable<TaskViewModel>>>>GetTaskBasedOnPriority(PriorityType priorityType)
        {
            try
            {
                var task = await taskService.GetTasksBasedOnPriority(priorityType); 
                var taskToReturn = mapper.Map<List<TaskViewModel>>(task);
                return Ok(new APIResponse<List<TaskViewModel>>
                { Status = true, 
                  Data = taskToReturn,
                  Message = "Tasks returned" 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("/Mark-Complete/{id:Guid}")]
        public async Task< ActionResult<APIResponse<TaskViewModel>>> MarkTaskAsComplete(Guid id)
        {
            try
            {
                var taskToComplete = taskService.MarKTaskComplete(id);
                var completedTask = mapper.Map<TaskViewModel>(taskToComplete);
                return Ok(new APIResponse<TaskViewModel>
                {
                    Status = true,
                    Data = completedTask,
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("/task-assignment")]
        public async Task<ActionResult<APIResponse<TaskViewModel>>> AssignTaskToUser([FromHeader] Guid userId,Guid taskId)
        {
            try
            {
                var assignedTask = taskService.AssignTaskToUser(userId,taskId);
                var task = mapper.Map<TaskViewModel>(assignedTask);
                return Ok(new APIResponse<TaskViewModel>
                {
                    Status = true,
                    Data = task,
                    Message = "Success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<object>
                {
                    Status = false,
                    Data = null,
                    Message= ex.Message,
                });
            }
        }
    }
}
