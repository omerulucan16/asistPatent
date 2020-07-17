using System;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using Microsoft.AspNetCore.Http;
using asistPatentCore.ViewModel;
using System.Text;

namespace asistPatentCore.Service
{
    public class CookieService : ICookieService
    {
        
        MainContext _mainContext = new MainContext();
        private readonly HttpContext _httpContext;
        public CookieService( HttpContext httpContext)
        {
            _httpContext = httpContext;
        }
        public bool setUserLogin(UsersViewModel model)
        {
            try
            {
                _httpContext.Session.Set("loggedUser", Encoding.UTF8.GetBytes("X"));
                _httpContext.Session.Set("name", Encoding.UTF8.GetBytes(model.userName));
                _httpContext.Session.Set("surname", Encoding.UTF8.GetBytes(model.userSurname));
                _httpContext.Session.Set("emailadress", Encoding.UTF8.GetBytes(model.userEmailAdress));
                _httpContext.Session.Set("id", Encoding.UTF8.GetBytes(model.userId.ToString()));
                _httpContext.Session.Set("status", Encoding.UTF8.GetBytes(model.status.ToString()));
                _httpContext.Session.Set("provider", Encoding.UTF8.GetBytes(model.provider.ToString()));
                //HttpContext.Current.Session.Add("name", model.name);
                //HttpContext.Current.Session.Add("name", model.name);
                //HttpContext.Current.Session.Add("name", model.name);
                //HttpContext.Current.Session.Add("name", model.name);

                //HttpContext.Current.Session.Add("name", model.name);

                //HttpContext.Current.Session.Add("surname", model.surname);
                //HttpContext.Current.Session.Add("emailadress", model.emailadress);
                //HttpContext.Current.Session.Add("id", model.id);
                //HttpContext.Current.Session.Add("status", model.roleLevel);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool checkUserLogin()
        {
            var loggedUserByte = default(byte[]);
            _httpContext.Session.TryGetValue("loggedUser", out loggedUserByte);
            string loggedUser = loggedUserByte == null ? "" :  Encoding.UTF8.GetString(loggedUserByte);
            if (loggedUser != "" && loggedUser == "X")
                return true;
            else
                return false;
        }
        public bool deleteSession()
        {
            try
            {
                _httpContext.Session.Clear();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
