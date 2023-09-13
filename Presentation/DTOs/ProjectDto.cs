using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOs
{
    public class ProjectDto
    {
        [Required]
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public ICollection<MyTask> myTasks { get; set; }
    }
    public class ProjectViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public ICollection<MyTask> myTasks { get; set; }
    }
}
