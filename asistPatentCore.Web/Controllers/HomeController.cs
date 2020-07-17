using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asistPatentCore.Web.Models;
using asistPatentCore.Service.IConstractor;
namespace asistPatentCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookieService _cookieService;
        public HomeController(ILogger<HomeController> logger,ICookieService cookieService)
        {
            _logger = logger;
            _cookieService = cookieService;
        }
        [Route("anasayfa")]
        public IActionResult Index()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            return View();
        }

       

        
    }
}
