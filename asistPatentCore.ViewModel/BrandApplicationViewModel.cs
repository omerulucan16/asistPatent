using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace asistPatentCore.ViewModel
{
    public class BrandApplicationViewModel
    {
        public UsersViewModel userInformation { get; set; }
        public IEnumerable<SelectListItem> ApplicationTypeList { get; set; }
        public ApplicationTypesEnum applicationType { get; set; } // yurtiçi yurtdışı
        public SubmitTokenEnum processToken { get; set; }
        public ApplicationCategoryTypesEnum applicationCategoryType { get; set; } // marka patent vb.
        public IEnumerable<SelectListItem> brandApplicationtypeList { get; set; }
        public ApplicationClassListViewModel applicationClassList { get; set; }
        public ApplicationSubClassListViewModel applicationSubClassList { get; set; }
        public BasketSaveStatusEnum saveStatus { get; set; } = BasketSaveStatusEnum.waiting;
        public BrandApplicationPricesViewModel prices { get; set; }
        public string subclassSearchText { get; set; }
        public CompaniesViewModel createCompany { get; set; }
        public string brandName { get; set; }
        public string explanation { get; set; }
        public List<IFormFile> uploadFiles { get; set; }
        public IEnumerable<SelectListItem> companieList { get; set; }
        public List<string> selectedCompany { get; set; }    
        public BrandApplicationVisibilityViewModel brandApplicationVisibilty { get; set; }
        public int selectedBrandApplication { get; set; }
    }
}
