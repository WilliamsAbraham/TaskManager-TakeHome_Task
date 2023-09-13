using System.ComponentModel.DataAnnotations;
using static Domain.StaticObjects.NotificationEnums;

namespace Presentation.DTOs
{
    public class NotificationDto
    {
        [Required]
        public NotificationType NotificationType { get; set; }
        [Required]
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
    public class NotificationViewModel
    {
        [Key]
        public Guid NotificationId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
