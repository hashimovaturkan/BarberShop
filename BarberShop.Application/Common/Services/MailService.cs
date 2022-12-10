using BarberShop.Application.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarberShop.Application.Common.Services
{
    public class MailService
    {
        private readonly IConfiguration Configuration;
      
        public Exception _sendMailException;

        public MailAddress _receiverMailAddress { get; set; }

        public MailService(string receiver, IConfiguration configuration)
        {
            Configuration = configuration;

            //_receiverMailAddress = new MailAddress(string.IsNullOrWhiteSpace(receiver) ? "test1@burncode.az" : receiver);
            _receiverMailAddress = new MailAddress("app@legacybarber.pl");

        }

        public async Task<bool> SendMail(string messageBody, string mailSubject, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(messageBody))
                throw new ArgumentNullException("email");

            string _senderMail = Configuration["MailConfig:senderMail"];
            string _senderMailPass = Configuration["MailConfig:senderMailPass"];
            MailAddress _senderMailAddress = new MailAddress(Configuration["MailConfig:senderMail"], "BarberShop", Encoding.UTF8);

            try
            {
                using (MailMessage mailMessage = new MailMessage(_senderMailAddress, _receiverMailAddress))
                {
                    mailMessage.Body = messageBody;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.Subject = mailSubject;
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;

                    using (SmtpClient smtpClient = new SmtpClient(Configuration["MailConfig:senderSmtp"], int.Parse(Configuration["MailConfig:port"])))
                    {
                        smtpClient.Credentials = new NetworkCredential(_senderMail, _senderMailPass);
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return true;
        }
    }
}
