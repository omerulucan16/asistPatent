using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class ApplicationSubClassViewModel
    {
        public int id { get; set; }
        public string subclasscode { get; set; }
        public string subclassname { get; set; }
        public GeneralStatusEnum status { get; set; }
        public int appclassId { get; set; }
        public string appclassName { get; set; }
        
        public bool isSelected { get; set; }

    }
    public class ApplicationSubClassListViewModel
    {
        public IList<ApplicationSubClassViewModel> subList { get; set; }

    }
}
