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
        //UsersViewModel getSessionUser();
        //IList<Users> getAdminUserList();
        //Users getSingleAdminUser(int id);
        //Boolean checkaddeduser(adminUsersViewModel model);
        //Boolean checkSpells(adminUsersViewModel model);
        //Boolean addorUpdateUser(adminUsersViewModel model);
        //Boolean saveMySettings(adminUsersViewModel model);
        //Boolean deleteUser(int id);
        //Boolean resendPassword(int id);
    }
}
