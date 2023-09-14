using Domain.Entities;
using infrastructure.Externals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public record EmailRequest(Notification Notification) : INotification;

    public class EmailHandler : INotificationHandler<EmailRequest>
    {
        private readonly IEmailSender emailSender;
        public EmailHandler(IEmailSender _sender)
        {
            emailSender = _sender;
        }

        public async Task Handle(EmailRequest notification, CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                emailSender.SendEmail(notification.Notification.To, notification.Notification.Type.ToString(), notification.Notification.Message);
            }
        }
    }

    
}
