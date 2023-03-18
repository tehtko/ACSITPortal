﻿using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace ACSITPortal.Services
{
    public class MailerService
    {
        private readonly IConfiguration _configuration;
        private SmtpClient smtpClient;
        public MailerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureMailSettings()
        {
            // Configure SMTP client
            smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("moderndsnsam@gmail.com", _configuration.GetSection("SMTP")["SMTPPassword"]);
        }
        public void SendEmail(string toAddress, string subject, string body)
        {
            ConfigureMailSettings();

            // Configure email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("moderndsnsam@gmail.com");
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Send email
            smtpClient.Send(mailMessage);
        }
    }
}
