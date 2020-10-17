using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.ViewModel
{
    public class MailSourceListViewModel
    {
        public IList<MailSourceViewModel> mailList { get; set; }
    }
    public class MailSourceViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string smtpServer { get; set; }
        public int port { get; set; }
        public string displayName { get; set; }
        public bool enableSsl { get; set; }
    }
}
