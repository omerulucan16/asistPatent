using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class ApplicationEmtiaClassViewModel
    {
        
        public int appSubClassId { get; set; }
        public int id { get; set; }
        public string value { get; set; }
        public string subclassname { get; set; }
        public GeneralStatusEnum status { get; set; }
       

    }
    public class ApplicationEmtiaClassListViewModel
    {
        public IList<ApplicationEmtiaClassViewModel> emtiaList { get; set; }

    }
}
