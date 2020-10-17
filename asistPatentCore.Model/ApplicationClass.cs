using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class ApplicationClass
    {
        public int id { get; set; } 
        public int appclassnumber { get; set; }
        public string appclassname { get; set; }
        public virtual ICollection<ApplicationSubClass> ApplicationSubClassesList { get; set; }
        public virtual ICollection<BasketClass> BasketClassesList { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
