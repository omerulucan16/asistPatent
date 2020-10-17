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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.Service
{
    public class ApplicationPricesService : IApplicationPricesService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUsersService _usersService;
        private readonly ICookieService _cookieService;
        private readonly IUploadImageService _uploadImageService;
        MainContext _mainContext = new MainContext();

        public ApplicationPricesService(IMapper mapper,IEmailService emailService,IUsersService usersService,ICookieService cookieService, IUploadImageService uploadImageService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _emailService = emailService;
            _cookieService = cookieService;
            _uploadImageService = uploadImageService;
        }

        public BrandApplicationPricesViewModel getPrices(int id)
        {
            return _mapper.Map<BrandApplicationPricesViewModel>(_mainContext.brandApplicationPrices.Where(w => w.brandAppTypeId == id).FirstOrDefault());

        }
        public double brandApplicationTotalPrice(BrandApplicationViewModel model) // marka başvuru total fiyat hesaplama
        {
            model.prices = getPrices(model.selectedBrandApplication);
            int totalSelectedClass = model.applicationClassList == null ? 0 : model.applicationClassList.applicationClassList.Where(w => w.isSelected).Count();

            double total = 0;
            if (totalSelectedClass > 0)
            {
                if (totalSelectedClass > 1)
                {
                    total = (model.prices.tuition * totalSelectedClass) + model.prices.service + (model.prices.extraClassService * totalSelectedClass);
                }
                else
                {
                    total = model.prices.tuition + model.prices.service;

                }
                total = (total * 118) / 100;
            }
            return total;

        }







    }
}