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
    public class MailSourcesController : Controller
    {
        private readonly IEmailService  _emailService;
        private readonly ICookieService _cookieService;

        public MailSourcesController(IEmailService emailService, ICookieService cookieService)
        {
            _cookieService = cookieService;
            _emailService = emailService;
        }
        [Route("~/admin/mail-kaynaklari")]
        public IActionResult Index()
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            return View(_emailService.getMailSourceList());
        }
        [Route("~/admin/mail-kaynaklari/{id:int}")]
        public IActionResult Detail(int id)
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            return View(_emailService.getMailSourceDetail(id));
        }
        [HttpPost]
        [Route("~/admin/mail-kaynaklari/{id:int}")]
        public IActionResult Detail(MailSourceViewModel model)
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            if (_emailService.updateMailSource(model))
                return View(model);
            else
                return Redirect("~/admin/mail-kaynaklari/"+model.id);
            
        }
        [Route("~/admin/mail-kaynaklari/yeni")]
        public IActionResult Create()
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            return View(new MailSourceViewModel());
        }
        [HttpPost]
        [Route("~/admin/mail-kaynaklari/yeni")]
        public IActionResult Create(MailSourceViewModel model)
        {
            if (!_cookieService.checkAdminState(Model.Enums.UserRoleEnum.supervisor))
            {
                return Redirect("~/anasayfa");
            }
            if (_emailService.createMailSource(model))
            {
                return Redirect("~/admin/mail-kaynaklari");
            }
            return View(model);
        }
    }
}