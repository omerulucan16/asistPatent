using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.ViewModel
{
    public class EmailTemplatesViewModel
    {
        public int id { get; set; }
        public string mailHeader { get; set; }
        public string mailContent { get; set; }
        public MailTemplatesEnum template { get; set; }
    }
    public class EmailTemplateListViewModel
    {
        public IList<EmailTemplatesViewModel> templateList { get; set; }
    }
}
