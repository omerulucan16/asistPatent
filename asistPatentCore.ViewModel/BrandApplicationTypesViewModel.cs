using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class BrandApplicationTypesViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public ApplicationTypesEnum applicationType { get; set; }
        public ApplicationCategoryTypesEnum categoryType { get; set; }
        public DateTime userCreateDate { get; set; }

    }
    public class BrandApplicationTypesListViewModel
    {
        public IList<BrandApplicationTypesViewModel> brandApplicationTypesList { get; set; }

    }
}
