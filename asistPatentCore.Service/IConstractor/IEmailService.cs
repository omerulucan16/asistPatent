using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface IEmailService
    {
        //MailSources GetMailSources();
        bool sendEmail(EmailViewModel model);
        EmailViewModel sendRegisterEmail(UsersViewModel registerModel);
    }
}
