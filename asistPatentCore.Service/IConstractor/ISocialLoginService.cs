using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface ISocialLoginService
    {
        UsersViewModel getUserInformation(string token, string userid);
        UsersViewModel getDataFromProvider(ProviderEnum provider, string token, string userid);

    }
}
