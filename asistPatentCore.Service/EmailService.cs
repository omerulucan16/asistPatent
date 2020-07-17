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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace asistPatentCore.Service
{
    public class EmailService : IEmailService
    {

        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        MainContext _mainContext = new MainContext();
        public EmailService(IMapper mapper)
        {
           // _usersService = usersService;
            _mapper = mapper;
        }

        MailSources GetMailSources()
        {
            return _mainContext.mailSources.FirstOrDefault();
        }
        public EmailViewModel sendRegisterEmail(UsersViewModel userModel)
        {
            EmailViewModel model = _mapper.Map<EmailViewModel>(_mainContext.mailTemplates.Where(w=>w.template == Model.Enums.MailTemplatesEnum.Register).FirstOrDefault());
            //model = _mapper.Map<EmailViewModel>(userModel);
            model.userName = userModel.userName;
            model.userSurname = userModel.userSurname;
            
            IList<string> userEmails = new List<string>();
            userEmails.Add(userModel.userEmailAdress);
            model.userEmailAdress = userEmails;
            model.mailContent = Regex.Replace(model.mailContent, "#ad#", model.userName);
            model.mailContent = Regex.Replace(model.mailContent, "#soyad#", model.userSurname);
            model.mailContent = Regex.Replace(model.mailContent, "#tokenid#", userModel.accessToken);
            model.mailContent = Regex.Replace(model.mailContent, "#web#", "https://online.asistpatent.com");
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
                foreach (var item in model.userEmailAdress)
                {
                    message.To.Add(new MailAddress(item));

                }

                message.Subject = model.mailHeader;
                message.IsBodyHtml = true;  
                message.Body = model.mailContent;
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
