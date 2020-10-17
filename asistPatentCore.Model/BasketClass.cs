using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class BasketClass
    {
        public int Id { get; set; }
        public int appClassId { get; set; }
        public virtual ApplicationClass ApplicationClass { get; set; }
        public Guid basketId { get; set; }
        public string basketClassName { get; set; }
        public string additionalValue { get; set; }
        public virtual Basket Basket { get; set; }
        public BasketStatusEnum status { get; set; }
        public DateTime createDate { get; set; }
    }
}
