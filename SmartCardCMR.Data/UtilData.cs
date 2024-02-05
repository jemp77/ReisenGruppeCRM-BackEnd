using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using SmartCardCRM.Util;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SmartCardCRM.Data
{
    public class UtilData : BaseData
    {
        private SendEmailDTO _SendEmail;
        public UtilData(SmartCardCRMContext context)
        {
            _context = context;
            _SendEmail = new SendEmailDTO
            {
                SMTP = _context.ConfigurationSettings.Where(x => x.Key == "SMTP").FirstOrDefault().Value,
                Port = int.Parse(_context.ConfigurationSettings.Where(x => x.Key == "SMTPPort").FirstOrDefault().Value),
                UserAuthentication = _context.ConfigurationSettings.Where(x => x.Key == "SMTPUserAut").FirstOrDefault().Value,
                UserPassword = _context.ConfigurationSettings.Where(x => x.Key == "SMTPUserPass").FirstOrDefault().Value,
                EnableSsl = bool.Parse(_context.ConfigurationSettings.Where(x => x.Key == "SMTPEnableSsl").FirstOrDefault().Value)
            };
        }

        public void SendWelcomeEmail(params string[] parameters)
        {
            new ExceptionLogData<bool>(_context).Log(this.GetType().Name,
                () =>
                {
                    _SendEmail.EmailFrom = _context.ConfigurationSettings.Where(x => x.Key == "EmailFrom").FirstOrDefault().Value;
                    _SendEmail.EmailFromDisplayName = _context.ConfigurationSettings.Where(x => x.Key == "EmailFromDisplayName").FirstOrDefault().Value;
                    _SendEmail.EmailsTo = parameters[0];
                    _SendEmail.Subject = string.Format(_context.ConfigurationSettings.Where(x => x.Key == "WelcomeEmailSubject").FirstOrDefault().Value, parameters[3]);
                    _SendEmail.Body = string.Format(_context.ConfigurationSettings.Where(x => x.Key == "WelcomeEmailBody").FirstOrDefault().Value, parameters[1], parameters[2], parameters[3]);
                    _SendEmail.Attachments = new List<Attachment> { new Attachment("./Documents/Manual y Carta de Bienvenida.pdf") };
                    new EmailNotifications().SendEmail(_SendEmail);
                    return true;
                });
        }

        public void SendEmail(params string[] parameters)
        {
            new ExceptionLogData<bool>(_context).Log(this.GetType().Name,
                () =>
                {
                    _SendEmail.EmailFrom = parameters[0];
                    _SendEmail.EmailFromDisplayName = parameters[1];
                    _SendEmail.EmailsTo = parameters[2];
                    _SendEmail.Subject = parameters[3];
                    _SendEmail.Body = parameters[4];
                    new EmailNotifications().SendEmail(_SendEmail);
                    return true;
                });
        }
    }
}
