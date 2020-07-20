using System;
using System.Collections.Generic;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface ICookieService
    {
        bool setUserLogin(UsersViewModel model);
       //bool deleteUserLogin();
        bool checkUserLogin();
        bool deleteSession();
        string getSessionEmail();
        bool checkAdminState(Model.Enums.UserRoleEnum role);
    }
}
