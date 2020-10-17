using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.Service.IConstractor
{
    public interface ICompaniesService
    {
        //MailSources GetMailSources();
        public IEnumerable<SelectListItem> GetCompanyList(Guid userId);
        bool createCompany(CompaniesViewModel model);

    }
}
