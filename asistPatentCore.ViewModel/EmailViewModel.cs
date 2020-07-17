using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.ViewModel
{
    public class EmailViewModel
    {
        public Guid userId { get; set; }
        public string mailHeader { get; set; }
        public string mailContent { get; set; }
        public MailTemplatesEnum template { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public IList<string> userEmailAdress { get; set; }
    }
}
