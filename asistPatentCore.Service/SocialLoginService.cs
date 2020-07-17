using System;
using AutoMapper;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using asistPatentCore.ViewModel;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace asistPatentCore.Service
{
    public class SocialLoginService : ISocialLoginService
    {
        private string uri_facebook = "https://graph.facebook.com/";
        private string uri_google = "https://oauth2.googleapis.com/tokeninfo?id_token=";
        private readonly IUsersService _usersService;
        public  SocialLoginService(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public UsersViewModel getUserInformation(string token, string userid)
        {
            UsersViewModel model = new UsersViewModel();
            if (userid == null || userid == "")
                model = getDataFromProvider(ProviderEnum.Google, token, userid);
            else
                model = getDataFromProvider(ProviderEnum.Facebook, token, userid);

            ProviderEnum provider = model.provider;
            if (_usersService.checkUserHave(model.userEmailAdress))// kullanıcı yok yarat.
            {
                
                model = _usersService.createSocialUser(model);
                
            }
            else
            {
                // kayitli kullanıcı var git onu getir.
                model = _usersService.getUserInformation(model.userEmailAdress);
            }
            model.provider = provider;
            return model;
        }
        public UsersViewModel getDataFromProvider(ProviderEnum provider,string token,string userid)
        {
            UsersViewModel model = new UsersViewModel();
            uri_facebook =uri_facebook+userid+ "?fields=email,first_name,last_name&access_token="+token;
            uri_google = uri_google + token;
            HttpClient http = new HttpClient();
            var data = http.GetAsync(provider == ProviderEnum.Facebook ? uri_facebook : uri_google).Result.Content.ReadAsStringAsync().Result;
            if (provider == ProviderEnum.Facebook)
            {
                UserFacebookViewModel facebookModel = JsonConvert.DeserializeObject<UserFacebookViewModel>(data);
                model = convertFacebook(facebookModel);
                
            }
            else
            {
                UserGoogleViewModel googleModel = JsonConvert.DeserializeObject<UserGoogleViewModel>(data);
                model = convertGoogle(googleModel);
                
            }

            return model;
            
        }
        UsersViewModel convertFacebook(UserFacebookViewModel model)
        {
            UsersViewModel usermodel = new UsersViewModel();
            usermodel.provider = ProviderEnum.Facebook;
            usermodel.userEmailAdress = model.email;
            usermodel.userName = model.first_name;
            usermodel.userSurname = model.last_name;
            return usermodel;
        }
        UsersViewModel convertGoogle(UserGoogleViewModel model)
        {
            UsersViewModel usermodel = new UsersViewModel();
            usermodel.provider = ProviderEnum.Google;
            usermodel.userEmailAdress = model.email;
            usermodel.userName = model.given_name;
            usermodel.userSurname = model.family_name;
            return usermodel;
        }
    }
}
