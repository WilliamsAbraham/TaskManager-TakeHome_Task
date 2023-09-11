using static Domain.StaticObjects.PriorityEnums;

namespace Presentation.DTOs
{
    public class TaskDto
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityType priorityType { get; set; } 
    }
}
