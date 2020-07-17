using System;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using Microsoft.AspNetCore.Http;
using asistPatentCore.ViewModel;
using System.Text;
using asistPatentCore.Model;
using System.Net.Mail;
using System.Net;
using AutoMapper;

namespace asistPatentCore.Service
{
    public class EmailService : IEmailService
    {

        private readonly IMapper _mapper;
        MainContext _mainContext = new MainContext();
        public EmailService(IMapper mapper)
        {

            _mapper = mapper;
        }

        MailSources GetMailSources()
        {
            return _mainContext.mailSources.FirstOrDefault();
        }
        public EmailViewModel sendRegisterEmail(RegisterViewModel registerModel)
        {
            EmailViewModel model = _mapper.Map<EmailViewModel>(_mainContext.mailTemplates.Where(w=>w.template == Model.Enums.MailTemplatesEnum.Register).FirstOrDefault());
            
            return model;
        }
        public bool sendEmail(EmailViewModel model)
        {
            try
            {
                MailSources mailsources = GetMailSources();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(mailsources.username);
                message.To.Add(new MailAddress("omerulucan@windowslive.com"));
               
                message.Subject = "Test";
                message.IsBodyHtml = true;  
                message.Body = "Mesaj içeriği";
                smtp.Port = mailsources.port;
                smtp.Host = mailsources.smtpServer;
                smtp.EnableSsl = mailsources.enableSsl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mailsources.username, mailsources.password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
