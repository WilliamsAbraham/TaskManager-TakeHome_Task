using static Domain.StaticObjects.NotificationEnums;

namespace Presentation.DTOs
{
    public class NotificationDto
    {
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
