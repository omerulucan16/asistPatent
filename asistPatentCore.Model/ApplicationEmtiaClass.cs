using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class ApplicationEmtiaClass
    {
        public int id { get; set; } 
        public string value { get; set; }
        public GeneralStatusEnum status { get; set; }
        public int appSubClassId { get; set; }
        public virtual ApplicationSubClass ApplicationSubClass { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
