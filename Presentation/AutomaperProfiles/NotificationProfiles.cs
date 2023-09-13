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
                CreateMap<Notification,NotificationViewModel>().ReverseMap();
                CreateMap<User,UserDto>().ReverseMap();
                CreateMap<User,UserViewModel>().ReverseMap();
                CreateMap<MyTask,TaskDto>().ReverseMap();
                CreateMap<MyTask,TaskViewModel>().ReverseMap();
                CreateMap<MyProject,ProjectDto>().ReverseMap();
                CreateMap<MyProject,ProjectViewModel>().ReverseMap();
        }
    }
}
