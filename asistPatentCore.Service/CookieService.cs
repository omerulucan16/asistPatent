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
        private readonly IUsersService _usersService;
        public CookieService( HttpContext httpContext,IUsersService usersService)
        {
            _httpContext = httpContext;
            _usersService = usersService;
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
       
        public bool checkAdminState(Model.Enums.UserRoleEnum role)
        {
            var loggedUserByte = default(byte[]);
            _httpContext.Session.TryGetValue("loggedUser", out loggedUserByte);
            string loggedUser = loggedUserByte == null ? "" : Encoding.UTF8.GetString(loggedUserByte);
            if (loggedUser != "" && loggedUser == "X")
            {
                string emailadress = getSessionEmail();
                UsersViewModel userModel = _usersService.getUserInformation(emailadress);
                if (userModel != null || userModel.userId != null)
                {
                    if (role == Model.Enums.UserRoleEnum.supervisor && userModel.role == Model.Enums.UserRoleEnum.supervisor)
                        return true;
                    else if (role == Model.Enums.UserRoleEnum.admin && userModel.role == Model.Enums.UserRoleEnum.admin)
                        return true;
                    else
                        return false;

                }
                else
                    return false;
                
            }
            else
                return false;
        }
        
        public string getSessionEmail()
        {
            var loggedUserByte = default(byte[]);
            _httpContext.Session.TryGetValue("emailadress", out loggedUserByte);
            string loggedUserEmail = loggedUserByte == null ? "" : Encoding.UTF8.GetString(loggedUserByte);
            return loggedUserEmail;
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
