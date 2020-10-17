using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class CompaniesViewModel
    {
        public int id { get; set; }
        public string companyTitle { get; set; }
        public string adress { get; set; }
        public string taxCenter { get; set; }
        public int taxNumber { get; set; }
        public int identyNumber { get; set; }
        public string companyTypeChoose { get; set; }
        public CompaniesTypeEnum companyType { get; set; }
        public Guid? userId { get; set; }
    } 
    public class CompaniesListViewModel
    {
        public IList<CompaniesViewModel> companieList { get; set; }

    }
}
