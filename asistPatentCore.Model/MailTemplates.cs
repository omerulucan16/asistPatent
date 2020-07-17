using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class MailTemplates
    {
        public int id { get; set; }
        public string mailHeader { get; set; }
        public string mailContent { get; set; }
        public MailTemplatesEnum template { get; set; }
    }
}
