using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface IEmailTemplateService
    {
        EmailTemplateListViewModel getEmailTemplateList();
        EmailTemplatesViewModel getEmailTemplateDetail(Model.Enums.MailTemplatesEnum type);
        void changeEmailTemplateDetail(EmailTemplatesViewModel model);
    }
}
