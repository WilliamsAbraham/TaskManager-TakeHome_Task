using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.NotificationEnums;

namespace Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public string To { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
