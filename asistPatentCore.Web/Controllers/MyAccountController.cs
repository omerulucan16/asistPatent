using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asistPatentCore.Web.Models;
using asistPatentCore.ViewModel;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Service;

namespace asistPatentCore.Web.Controllers
{
    public class MyAccountController : Controller
    {
        private readonly IMyAccountService _myAccountService;
        private readonly ICookieService _cookieService;
        private readonly IUsersService _usersService;
        public MyAccountController(IMyAccountService myAccountService, ICookieService cookieService,IUsersService usersService)
        {
            _cookieService = cookieService;
            _myAccountService = myAccountService;
            _usersService = usersService;
        }
        [Route("~/hesabim/bilgilerimi-guncelle")]
        public IActionResult Index()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            MyAccountViewModel model = _myAccountService.getPersonnelInformation(_cookieService.getSessionEmail());
            return View(model);
        }
        [HttpPost]
        [Route("~/hesabim/bilgilerimi-guncelle")]
        public IActionResult Index(MyAccountViewModel model)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            _myAccountService.updatePersonnelInformation(model);
            return Redirect("~/hesabim/bilgilerimi-guncelle");
        }
        [Route("~/hesabim/sifremi-guncelle")]
        public IActionResult PasswordUpdate()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            //MyAccountViewModel model = _myAccountService.getPersonnelInformation(_cookieService.getSessionEmail());

            return View(new MyAccountViewModel());
        }
        [HttpPost]
        [Route("~/hesabim/sifremi-guncelle")]
        public IActionResult PasswordUpdate(MyAccountViewModel model)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            model.userEmailAdress = _cookieService.getSessionEmail();
            if (_usersService.changeRegisterUserPasswordWithEmail(model))
            {
                return Redirect("~/anasayfa");
            }
            //MyAccountViewModel model = _myAccountService.getPersonnelInformation(_cookieService.getSessionEmail());
            
            return View(new MyAccountViewModel());
        }
    }
}