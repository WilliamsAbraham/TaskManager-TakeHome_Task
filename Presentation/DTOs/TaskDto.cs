using System.ComponentModel.DataAnnotations;
using static Domain.StaticObjects.PriorityEnums;

namespace Presentation.DTOs
{
    public class TaskDto
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityType priorityType { get; set; } 
    }

    public class TaskViewModel
    {
        [Key]
        public Guid TaskId { get; set; }
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
