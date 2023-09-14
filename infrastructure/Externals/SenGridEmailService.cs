using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace infrastructure.Externals
{
    public class SenGridEmailService : IEmailSender
    {
        private readonly SendGridClient sendGridClient;
        public SenGridEmailService(SendGridClient _sendGridClient)
        {
           sendGridClient = _sendGridClient; 
        }
        public void SendEmail(string to, string subject, string message)
        {
            var from = new EmailAddress("taskmanagementApi@gmail.com","taskmanagement Api.IO");
            var gpingto = new EmailAddress(to, "");
            var msg = MailHelper.CreateSingleEmail(from,gpingto,subject,message,"");

            var response = sendGridClient.SendEmailAsync(msg);
            

            if (response.IsCompletedSuccessfully)
            {
                
            }
            else
            {
               
            }
        }
    }
}
