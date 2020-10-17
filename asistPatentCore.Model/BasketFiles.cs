using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class BasketFiles
    {
        public int Id { get; set; }
        public string filename { get; set; }
        public Guid basketId { get; set; }
        public virtual Basket Basket { get; set; }
        public BasketStatusEnum status { get; set; }
        public DateTime createDate { get; set; }
    }
}
