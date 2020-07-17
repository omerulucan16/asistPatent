using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.ViewModel
{
    public class EmailViewModel
    {
        public string mailHeader { get; set; }
        public string mailContent { get; set; }
        public MailTemplatesEnum template { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string userEmailAdress { get; set; }
    }
}
