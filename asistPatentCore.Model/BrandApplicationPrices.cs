using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class BrandApplicationPrices
    {
        public int id { get; set; }
        public int brandAppTypeId { get; set; }
        public virtual BrandApplicationTypes BrandApplicationTypes { get; set; }
        public double tuition { get; set; }
        public double service { get; set; }
        public double extraClassService { get; set; }
        public double branch { get; set; }
        public DateTime userCreateDate { get; set; }
    }
}
