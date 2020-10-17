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
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly ICookieService _cookieService;
        private readonly IBasketService _basketService;
        public BasketController(ILogger<BasketController> logger,ICookieService cookieService,IBasketService basketService)
        {
            _logger = logger;
            _cookieService = cookieService;
            _basketService = basketService;
        }
        [Route("sepetim")]
        public IActionResult Index()
        {
            if (!_cookieService.checkUserLogin())
            {
                return Redirect("~/giris");
            }
            return View(_basketService.myBasketList());
        }
        [HttpGet]
        [Route("sepetim/sil/{basketId}")]
        public ActionResult Delete(Guid basketId)
        {
            _basketService.deleteBasketItem(basketId);
            return Redirect("~/sepetim");
        }

        
    }
}
