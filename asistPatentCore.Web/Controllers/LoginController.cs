using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
using asistPatentCore.Service;
using Microsoft.AspNetCore.Http;
using asistPatentCore.Service.IConstractor;
using AutoMapper;

namespace asistPatentCore.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly IUsersService _usersService;
        private readonly ISocialLoginService _socialLoginService;
        private readonly IDefaultValuesService _defaultValuesService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public LoginController(ICookieService cookieService,IEmailService emailService,IUsersService usersService,ISocialLoginService socialLoginService,IDefaultValuesService defaultValuesService,IMapper mapper)
        {
            _usersService = usersService;
            _defaultValuesService = defaultValuesService;
            _cookieService = cookieService;
            _socialLoginService = socialLoginService;
            _emailService = emailService;
            _mapper = mapper;
        }
        [Route("giris")]
        [Route("~/")]
        public IActionResult Index()
        {
            
            UsersViewModel model = new UsersViewModel();
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            //_emailService.sendEmail(null);
            //ToastrService.AddToUserQueue(new Toastr("Test", type: Model.Enums.ToastrType.Warning));
            return View(model); 
        }
        [HttpPost]
        [Route("~/giris")]
        public IActionResult signIn(UsersViewModel model)
        {
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            if (_usersService.loginUser(model.userEmailAdress, model.userPassword))
            {
                model = _usersService.getUserInformation(model.userEmailAdress);
                model.provider = Model.Enums.ProviderEnum.Pure;
                if (_cookieService.setUserLogin(model))
                {
                    return Redirect("~/anasayfa");
                }
                else
                {
                    return Redirect("~/giris");
                }
                
            }
            else
            {
                return Redirect("~/giris");
            }

            
        }
        [HttpPost]
        [Route("~/signinSocial")]
        public JsonResult signInSocial(string token , string userid )
        {
            if(_cookieService.checkUserLogin())
            {
                return Json("Logged");
            }

            UsersViewModel model =_socialLoginService.getUserInformation(token, userid);
            if (_cookieService.setUserLogin(model))
            {
                return Json("Ok");
            }
            else
            {
                return Json("Nok");
            }
            

        }
        [Route("~/sifremi-unuttum")]
        public IActionResult forgetPass()
        {
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            UsersViewModel model = new UsersViewModel();
            return View(model);
        }
        [HttpPost]
        [Route("~/sifremi-unuttum")]
        public IActionResult forgetPass(UsersViewModel model)
        {
            
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            if (_usersService.forgetPassSend(model.userEmailAdress))
            {
                model.isRegistered = true;
            }
            return View(model);
        }
        [Route("~/sifremi-sifirla/{tokenId}")]
        public IActionResult passReset(Guid tokenId)
        {
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            RegisterViewModel passResetModel = new RegisterViewModel();
            if (!_usersService.checkForgetPassToken(tokenId))
            {
                return Redirect("~/giris");
            }
            passResetModel.accessToken = tokenId.ToString();
            return View(passResetModel);
        }
        [HttpPost]
        public IActionResult passResetPost(RegisterViewModel model)
        {
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            
            if (_usersService.changeRegisterUserPassword(model))
            {
                return Redirect("~/giris");
            }
            model.userPassword = "*";
            model.userRePassword = "*";
            return Redirect("~/sifremi-sifirla/"+model.accessToken);
        }

        public IActionResult Logout()
        {
            _cookieService.deleteSession();
            return Redirect("~/");
        }
        
    }
}