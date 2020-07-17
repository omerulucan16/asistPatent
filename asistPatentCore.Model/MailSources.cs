using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class MailSources
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string smtpServer { get; set; }
        public int port { get; set; }
        public bool enableSsl { get; set; }
    }
}
