using SmartCardCRM.Model.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SmartCardCRM.Util
{
    public class EmailNotifications
    {
        public void SendEmail(SendEmailDTO sendEmail)
        {
            var mailMessage = new MailMessage();
            var client = new SmtpClient(sendEmail.SMTP, sendEmail.Port)
            {
                Credentials = new NetworkCredential(sendEmail.UserAuthentication, sendEmail.UserPassword),
                EnableSsl = true
            };
                
            mailMessage.From = new MailAddress(sendEmail.EmailFrom, sendEmail.EmailFromDisplayName);
            sendEmail.EmailsTo.Split(";").ToList().ForEach(x => mailMessage.To.Add(x));
            mailMessage.Subject = sendEmail.Subject;
            mailMessage.Body = sendEmail.Body;
            mailMessage.IsBodyHtml = true;
            if (sendEmail.Attachments != null && sendEmail.Attachments.Any())
            {
                sendEmail.Attachments.ForEach(x => mailMessage.Attachments.Add(x));
            }

            client.Send(mailMessage);
        }
    }
}
