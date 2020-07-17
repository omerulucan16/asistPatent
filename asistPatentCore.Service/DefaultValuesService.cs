using System;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using Microsoft.AspNetCore.Http;
using asistPatentCore.ViewModel;
using System.Text;

namespace asistPatentCore.Service
{
    public class DefaultValuesService : IDefaultValuesService
    {
        
        MainContext _mainContext = new MainContext();
        
        public DefaultValuesService( )
        {
            
        }
        public string getDefaultValue(string key)
        {
            try
            {
                return _mainContext.defaultValues.Where(w => w.key == key).FirstOrDefault().value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        //public UsersViewModel getSessionUser()
        //{
        //    UsersViewModel model = new UsersViewModel();
        //    try
        //    {
        //        model.id = Convert.ToInt32(HttpContext.Current.Session["id"].ToString());
        //        model.surname = HttpContext.Current.Session["surname"].ToString();
        //        model.emailadress = HttpContext.Current.Session["emailadress"].ToString();
        //        model.name = HttpContext.Current.Session["name"].ToString();
        //        if (HttpContext.Current.Session["status"].ToString() == "admin")
        //        {
        //            model.roleLevel = Data.Enums.adminUsersRoleEnum.admin;
        //        }
        //        else if (HttpContext.Current.Session["status"].ToString() == "supervisor")
        //        {
        //            model.roleLevel = Data.Enums.adminUsersRoleEnum.supervisor;
        //        }

        //    }
        //    catch (Exception)
        //    {


        //    }
        //    return model;

        //}
    }
}
