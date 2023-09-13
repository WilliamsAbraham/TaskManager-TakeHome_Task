using Application.Services.NotificationService;
using AutoMapper;
using Domain.Entities;
using Domain.StaticObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System.Collections;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
       private readonly NotificationRepository notificationRepository;
        private readonly IMapper mapper;
        public NotificationController(NotificationRepository _notificationRepository, IMapper _mapper)
        {
            notificationRepository = _notificationRepository;
            mapper = _mapper;
                
        }

        //Get All Notifications
        [HttpGet("/AllNotifications")]
        public async Task<ActionResult<APIResponse<List<NotificationDto>>>> GetAllNotifications(CancellationToken cancellationToken)
        {
            var notifications = await notificationRepository.GetAllNotice(cancellationToken);
            var notices = mapper.Map<List<NotificationDto>>(notifications);
            return Ok(new APIResponse<List<NotificationDto>>
            {
                Status = true,
                Data = notices,
                Message = "Notifications retrived successfully",
            });
            
        }

        //GET: NotificationController/5
        [HttpGet("/Notification/{Id:Guid}")]
        public async Task<ActionResult<APIResponse<NotificationDto>>> GetNotificationById(Guid id)
        {
            var notification = await notificationRepository.GetNoticeById(id);
            var notice = mapper.Map<NotificationDto>(notification);
            return Ok(new APIResponse<NotificationDto>
            {
                Status = true,
                Data = notice,
                Message = "Notice retrived successfully"
            });
        }

        // POST: NotificationController/Create
        [HttpPost("/new-notification")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<APIResponse<string>>> CreateNotification(NotificationDto notificationDto)
        {
            var notification = mapper.Map<Notification>(notificationDto);
            try
            {
               await notificationRepository.CreateNotification(notification);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Notification Created Successfully"
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
        [HttpPut("/EditNtification/{Id:Guid}")]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult<APIResponse<string>>> Edit(Guid id,NotificationDto notificationDto)
        {
            var noticeToUpdate = mapper.Map<Notification>(notificationDto);
            try
            {
                await notificationRepository.UpdateNotification(id,noticeToUpdate);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Notification Updated successfully"
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
        [HttpPost("/NotificationDeletion/{Id:Guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<APIResponse<string>>> Delete(Guid id)
        {
            //var noticeToDelete = mapper.Map<Notification>(notificationDto);

            try
            {
                 await notificationRepository.DeleteNotification(id);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "Notification deleted successfully"
                });
            }
            catch(Exception ex) 
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
