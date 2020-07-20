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
    public class EmailTemplateService : IEmailTemplateService
    {

        private readonly IMapper _mapper;
        MainContext _mainContext = new MainContext();
        public EmailTemplateService(IMapper mapper)
        {
           // _usersService = usersService;
            _mapper = mapper;
           
        }
        public EmailTemplateListViewModel getEmailTemplateList()
        {
            EmailTemplateListViewModel model = new EmailTemplateListViewModel();
            model.templateList = _mapper.Map<IList<EmailTemplatesViewModel>>(_mainContext.mailTemplates.ToList());
            return model;
        }
        public EmailTemplatesViewModel getEmailTemplateDetail(Model.Enums.MailTemplatesEnum type)
        {
            return _mapper.Map<EmailTemplatesViewModel>(_mainContext.mailTemplates.Where(w => w.template == type).FirstOrDefault());
        }
        public void changeEmailTemplateDetail(EmailTemplatesViewModel model)
        {
            MailTemplates mailTemplatesModel = _mainContext.mailTemplates.Where(w => w.id == model.id).FirstOrDefault();
            mailTemplatesModel.mailContent = model.mailContent;
            mailTemplatesModel.mailHeader = model.mailHeader;
            if (_mainContext.SaveChanges() == 1)
                ToastrService.AddToUserQueue(new Toastr("Tebrikler e-posta taslağınızı başarıyla güncellediniz.", type: Model.Enums.ToastrType.Success));
            else
                ToastrService.AddToUserQueue(new Toastr("E-posta taslağınızı güncellerken bir hata gerçekleşti.", type: Model.Enums.ToastrType.Error));

        }
    }
}
