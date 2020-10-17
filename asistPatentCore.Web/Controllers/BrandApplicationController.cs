using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace asistPatentCore.Web.Controllers
{
    public class BrandApplicationController : Controller
    {
        private readonly IBrandApplicationService _brandApplicationService;
        public BrandApplicationController(IBrandApplicationService brandApplicationService)
        {
            _brandApplicationService = brandApplicationService;
        }
        [Route("~/markalarim")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("~/markalarim/yeni-basvur")]
        public IActionResult Create()
        {
            BrandApplicationViewModel model = _brandApplicationService.getFirstInformationForApplication();
            return View(model);
        }
        [HttpPost]
        [Route("~/markalarim/yeni-basvur")]
        public IActionResult Create(BrandApplicationViewModel model)
        {
            BrandApplicationViewModel returnModel = _brandApplicationService.brandApplicationPost(model);
            if(returnModel.saveStatus == Model.Enums.BasketSaveStatusEnum.success)
            {
                return Redirect("~/markalarim/yeni-basvur");
            }
            ModelState.Clear();
            return View(returnModel);
        }
       

    }
}