using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace asistPatentCore.Web.Controllers
{
    public class AuthorizedUserController : Controller
    {
        private readonly IDefaultValuesService _defaultValuesService;
        private readonly ICookieService _cookieService;
        private readonly IAuthorizedUserService _authorizedUserService;
        private readonly IUsersService _usersService;
        public AuthorizedUserController(IUsersService usersService, IDefaultValuesService defaultValuesService, ICookieService cookieService, IAuthorizedUserService authorizedUserService)
        {
            _usersService = usersService;
            _defaultValuesService = defaultValuesService;
            _cookieService = cookieService;
            _authorizedUserService = authorizedUserService;
        }
        [Route("~/hesabim/yetkililer")]
        public IActionResult Index()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            return View(_authorizedUserService.getAuthorizedUserList(_usersService.getUserInformation(_cookieService.getSessionEmail()).userId));
        }
        [Route("~/hesabim/yetkililer/{authId}")]
        public IActionResult Detail(Guid authId)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            return View(_authorizedUserService.detailAuthorizedUser(authId, _usersService.getUserInformation(_cookieService.getSessionEmail()).userId));
        }
        [HttpPost]
        [Route("~/hesabim/yetkililer/{authId}")]
        public IActionResult Detail(UsersViewModel model)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            if (_authorizedUserService.updateAuthorizedUser(model, _usersService.getUserInformation(_cookieService.getSessionEmail()).userId))
                return View(model);
            else
                return Redirect("~/hesabim/yetkililer/"+model.authId);
        }
        [Route("~/hesabim/yetkililer/yeni")]
        public IActionResult Create()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            return View(new UsersViewModel());
        }
        [HttpPost]
        [Route("~/hesabim/yetkililer/yeni")]
        public IActionResult Create(UsersViewModel model)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            if (!_authorizedUserService.addAuthorizedUser(model, _usersService.getUserInformation(_cookieService.getSessionEmail()).userId))
            {
                return View(model);
            }
            return Redirect("~/hesabim/yetkililer");
        }
        [Route("~/hesabim/yetkili-sil/{authId}")]
        public IActionResult Delete(Guid authId)
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
                
            }
            if(!_authorizedUserService.deleteAuthorizedUser(authId, _usersService.getUserInformation(_cookieService.getSessionEmail()).userId))
            {
                return Redirect("~/hesabim/yetkililer/" + authId);
            }
            return Redirect("~/hesabim/yetkililer");
        }
    }
}