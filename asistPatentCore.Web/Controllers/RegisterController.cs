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

namespace asistPatentCore.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly IUsersService _usersService;
        private readonly ISocialLoginService _socialLoginService;
        private readonly IDefaultValuesService _defaultValuesService;
        private readonly IEmailService _emailService;
        public RegisterController(ICookieService cookieService, IEmailService emailService, IUsersService usersService, ISocialLoginService socialLoginService, IDefaultValuesService defaultValuesService)
        {
            _usersService = usersService;
            _defaultValuesService = defaultValuesService;
            _cookieService = cookieService;
            _socialLoginService = socialLoginService;
            _emailService = emailService;
        }
        [Route("~/kayit-ol")]
        public IActionResult Index()
        {
            RegisterViewModel model = new RegisterViewModel();
            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }
            model.termsconditionMessage = _defaultValuesService.getDefaultValue("uyeliksozlesmesi");
            return View(model);
        }
        [HttpPost]
        [Route("~/kayit-ol")]
        public IActionResult Index(RegisterViewModel model)
        {

            if (_cookieService.checkUserLogin())
            {
                return Redirect("~/anasayfa");
            }

            if (_usersService.registerUser(model))
            {
                UsersViewModel registeredModel = _usersService.getUserInformation(model.userEmailAdress);
                registeredModel.accessToken = _usersService.getUserToken(Model.Enums.UsersTokenTypeEnum.register, registeredModel.userId);
                if (_emailService.sendEmail(_emailService.sendRegisterEmail(registeredModel)))
                {
                    model.userId = registeredModel.userId;
                }
                model.isRegistered = true;
            }
            return View(model);
        }
        [HttpGet]
        [Route("~/email-onay/{userToken}")]
        public IActionResult registerApprove(Guid userToken)
        {
            _usersService.checkRegisterToken(userToken);
            return Redirect("~/giris");
        }
    }
}