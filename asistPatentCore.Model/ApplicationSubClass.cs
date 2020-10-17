using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class ApplicationSubClass
    {
        public int id { get; set; } 
        public string subclasscode { get; set; }
        public string subclassname { get; set; }
        public GeneralStatusEnum status { get; set; }
        public int appclassId { get; set; }
        public virtual ApplicationClass ApplicationClass { get; set; }
        public virtual ICollection<ApplicationEmtiaClass> ApplicationEmtiaClassesList { get; set; }
        public virtual ICollection<BasketSubClass> BasketSubClassRelation { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
