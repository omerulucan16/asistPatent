using System;
using asistPatentCore.Data;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface IBrandApplicationService
    {
        BrandApplicationViewModel getFirstInformationForApplication();
        BrandApplicationViewModel brandApplicationPost(BrandApplicationViewModel model);
        bool checkSaveBasketPossibilty(BrandApplicationViewModel Checkmodel);
    }
}
