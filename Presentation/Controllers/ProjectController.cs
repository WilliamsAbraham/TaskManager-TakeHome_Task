using Application.Services.NotificationService;
using Application.Services.ProjectService;
using AutoMapper;
using Domain.Entities;
using Domain.StaticObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectRepository projectRepository;
        private readonly IMapper mapper;
        public ProjectController(ProjectRepository _projectRepository, IMapper _mapper)
        {
            projectRepository = _projectRepository;
            mapper = _mapper;

        }

        //Get All Notifications
        [HttpGet("/AllProject")]
        public async Task<ActionResult<APIResponse<List<ProjectViewModel>>>> GetAllProject(CancellationToken cancellationToken)
        {
            var projectsRetrieved = await projectRepository.GetAllProjects(cancellationToken);
            var projects = mapper.Map<List<ProjectViewModel>>(projectsRetrieved);
            return Ok(new APIResponse<List<ProjectViewModel>>
            {
                Status = true,
                Data = projects,
                Message = "projects retrived successfully",
            });

        }

        //GET: NotificationController/5
        [HttpGet("/Project/{id:Guid}")]
        public async Task<ActionResult<APIResponse<ProjectViewModel>>> GetProjectById(Guid id)
        {
            var projectRetrieved = await projectRepository.GetProjectById(id);
            var project = mapper.Map<ProjectViewModel>(projectRetrieved);
            return Ok(new APIResponse<ProjectViewModel>
            {
                Status = true,
                Data = project,
                Message = "Project retrived successfully"
            });
        }

        // POST: NotificationController/Create
        [HttpPost("/new-project")]
        public async Task<ActionResult<APIResponse<string>>> CreateProject(ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "please correct the errors");
            }
            var project = mapper.Map<MyProject>(projectDto);
            try
            {
                await projectRepository.CreateProject(project);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Project Created Successfully"
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

        //// PUT: NotificationController/Edit/5
        [HttpPut("/EditProject/{id:Guid}")]
        public async Task<ActionResult<APIResponse<string>>> Edit(Guid id, ProjectDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "please correct the errors");
            }
            var projectToUpdate = mapper.Map<MyProject>(projectDto);
            try
            {
                await projectRepository.UpdateProject(id, projectToUpdate);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Project Updated successfully"
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

        //// POST: NotificationController/Delete/5
        [HttpPost("/ProjectDeletion/{id:Guid}")]
        public async Task<ActionResult<APIResponse<string>>> Delete(Guid id)
        {
            //var noticeToDelete = mapper.Map<Notification>(notificationDto);

            try
            {
                await projectRepository.DeleteProject(id);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Project deleted successfully"
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
    }
}
