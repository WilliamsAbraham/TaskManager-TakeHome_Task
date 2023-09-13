using AutoMapper;
using Domain.Entities;
using Presentation.DTOs;

namespace Presentation.AutomaperProfiles
{
    public class AutomapperProfiles :Profile
    {
        public AutomapperProfiles()
        {
                CreateMap<Notification,NotificationDto>().ReverseMap();
                CreateMap<User,UserDto>().ReverseMap();
                CreateMap<MyTask,TaskDto>().ReverseMap();
                CreateMap<MyProject,ProjectDto>().ReverseMap();
        }
    }
}
