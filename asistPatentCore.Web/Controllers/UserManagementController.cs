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
    public class UserManagementController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly IUsersService _usersService;
        public UserManagementController(ICookieService cookieService, IUsersService usersService)
        {
            _cookieService = cookieService;
            _usersService = usersService;
        }

        [Route("~/admin/kullanicilari-yonet")]
        public IActionResult Index()
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }

            return View(_usersService.getUsersList());
        }
        [Route("~/admin/kullanicilari-yonet/{userId}")]
        public IActionResult Index(Guid userId)
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            ;
            if (_usersService.forgetPassSend(_usersService.getUserInformationFromId(userId).userEmailAdress))
            {
                ToastrService.AddToUserQueue(new Toastr("Şifre sıfırlama bilgileri kullanıcıya iletilmiştir.", type: Model.Enums.ToastrType.Success));
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Şifre sıfırlama iletisi gönderilirken bir hata oluştu.Lütfen tekrar deneyiniz.", type: Model.Enums.ToastrType.Error));
            }
            return Redirect("~/admin/kullanicilari-yonet");
        }

    }
}