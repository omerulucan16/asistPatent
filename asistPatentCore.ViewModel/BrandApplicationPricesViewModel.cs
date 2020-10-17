using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class BrandApplicationPricesViewModel
    {
        public int id { get; set; }
        public int brandAppTypeId { get; set; }
        public double tuition { get; set; }
        public double service { get; set; }
        public double extraClassService { get; set; }
        public double totalPrice { get; set; }
        public double branch { get; set; }

    }
   
}
