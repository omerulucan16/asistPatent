using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.Service.IConstractor
{
    public interface IApplicationPricesService
    {
        double brandApplicationTotalPrice(BrandApplicationViewModel model);
        BrandApplicationPricesViewModel getPrices(int id);
    }
}
