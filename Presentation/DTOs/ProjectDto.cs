using Domain.Entities;

namespace Presentation.DTOs
{
    public class ProjectDto
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public ICollection<MyTask> myTasks { get; set; }
    }
}
