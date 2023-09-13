using Application.Services.NotificationService;
using Application.Services.UserService;
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
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;
        public UserController(UserRepository _userRepo, IMapper _mapper)
        {
            this.userRepository = _userRepo;
            mapper = _mapper;
        }
        [HttpGet("/AllUsers")]
        public async Task<ActionResult<APIResponse<List<UserDto>>>> GetAllUsers(CancellationToken cancellationToken)
        {
            var usersRetrievd = await userRepository.GetAllUsers(cancellationToken);
            var users = mapper.Map<List<UserDto>>(usersRetrievd);
            return Ok(new APIResponse<List<UserDto>>
            {
                Status = true,
                Data = users,
                Message = "Users retrived successfully",
            });

        }


        //GET: NotificationController/5
        [HttpGet("/user/{Id:Guid}")]
        public async Task<ActionResult<APIResponse<UserDto>>> GetUserById(Guid id)
        {
            var userRetrieved = await userRepository.GetUserById(id);
            var user = mapper.Map<UserDto>(userRetrieved);
            return Ok(new APIResponse<UserDto>
            {
                Status = true,
                Data = user,
                Message = "User retrived successfully"
            });
        }

        // POST: NotificationController/Create
        [HttpPost("/new-User")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<APIResponse<string>>> CreateUser(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            try
            {
                await userRepository.CreateUser(user);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "User Created Successfully"
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
        [HttpPut("/user/{Id:Guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<APIResponse<string>>> Edit(Guid id,UserDto userDto)
        {
            var userToUpdate = mapper.Map<User>(userDto);
            try
            {
                await userRepository.UpdateUser(id,userToUpdate);
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
        [HttpPost("/UserDeletion/{Id:Guid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<APIResponse<string>>> Delete(Guid id,UserDto userDto)
        {
            var userToDelete = mapper.Map<User>(userDto);

            try
            {
                await userRepository.DeleteUser(id);
                return Ok(new APIResponse<string>
                {
                    Status = true,
                    Data = null,
                    Message = "user deleted successfully"
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
