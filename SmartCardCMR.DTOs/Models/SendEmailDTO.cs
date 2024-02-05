using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace SmartCardCRM.Model.Models
{
    public class SendEmailDTO
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string UserAuthentication { get; set; }
        public string UserPassword { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromDisplayName { get; set; }
        public string EmailsTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
