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
    public class BrandApplicationService : IBrandApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly ICookieService _cookieService;
        private readonly ICompaniesService _companiesService;
        private readonly IBrandApplicationClassesService _brandApplicationClassesService;
        private readonly IBasketService _basketService;
        private readonly IApplicationPricesService _applicationPricesService;
        MainContext _mainContext = new MainContext();
       
        
        public BrandApplicationService(IMapper mapper,IUsersService usersService,ICookieService cookieService,ICompaniesService companiesService,IBrandApplicationClassesService brandApplicationClassesService,IBasketService basketService,IApplicationPricesService applicationPricesService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _cookieService = cookieService;
            _companiesService = companiesService;
            _brandApplicationClassesService = brandApplicationClassesService;
            _basketService = basketService;
            _applicationPricesService = applicationPricesService;
        }
        BrandApplicationViewModel getBrandAppStandartValues(BrandApplicationViewModel model)
        {
            model.brandApplicationVisibilty = new BrandApplicationVisibilityViewModel();
            model.ApplicationTypeList = ((ApplicationTypesEnum[])Enum.GetValues(typeof(ApplicationTypesEnum))).Select(c => new SelectListItem() { Value = ((int)c).ToString(), Text = c.GetDescription() }).ToList();
            model.userInformation = _usersService.getUserInformation(_cookieService.getSessionEmail());
            model.brandApplicationtypeList = _mainContext.brandApplicationTypes.Where(w => w.applicationType == model.applicationType && w.categoryType == ApplicationCategoryTypesEnum.brand).Select(s => new SelectListItem()
            {
                Text = s.name,
                Value = s.id.ToString()
            }).ToList();
            return model; // burası hep standart
        }
        public BrandApplicationViewModel getFirstInformationForApplication()
        {
            BrandApplicationViewModel model = new BrandApplicationViewModel();
            
            model.ApplicationTypeList = ((ApplicationTypesEnum[])Enum.GetValues(typeof(ApplicationTypesEnum))).Select(c => new SelectListItem() { Value = ((int)c).ToString(), Text = c.GetDescription() }).ToList();
            model.applicationType = ApplicationTypesEnum.domestic;
            model.userInformation = _usersService.getUserInformation(_cookieService.getSessionEmail());
            model.brandApplicationtypeList = _mainContext.brandApplicationTypes.Where(w => w.applicationType == ApplicationTypesEnum.domestic && w.categoryType == ApplicationCategoryTypesEnum.brand).Select(s => new SelectListItem()
            {
                Text=s.name,
                Value=s.id.ToString()
            }).ToList();
            model.selectedBrandApplication = 0;
            return model;
        }





        public BrandApplicationViewModel brandApplicationPost(BrandApplicationViewModel model) // herşey burda başlıyor
        {
            if (model.processToken == SubmitTokenEnum.applicationType) // yurtiçi yurtdışı seçtiyse 
            {
               return  getInformationForApplicationTypeChoose(model);
            }
            else if (model.processToken == SubmitTokenEnum.brandApplicationType) // işlem seçildiğinde
            {
                return getInformationForBrandApplicationTypeChoose(model);
            }
            else if(model.processToken == SubmitTokenEnum.classSearchType) // işlem marka başvurusu && arama yap dediyse 
            {
                model.applicationClassList = new ApplicationClassListViewModel();
                model.applicationSubClassList = new ApplicationSubClassListViewModel();
               // returnModel.prices.totalPrice = _applicationPricesService.brandApplicationTotalPrice(model);
                return getInformationForBrandApplicationTypeChoose(model);
            }
            else if (model.processToken == SubmitTokenEnum.companyAddType) // firma ekled
            {
                if(_companiesService.createCompany(model.createCompany))
                {
                    model.createCompany = new CompaniesViewModel();
                }
                BrandApplicationViewModel returnModel = getInformationForCreateCompanyChoose(model);
                return returnModel;

            }
            else if(model.processToken == SubmitTokenEnum.submit)
            {
               
                return submitBrandApplication(model);
            }
           
            else
            {
                return null;
            }
        }

        
        BrandApplicationViewModel getInformationForApplicationTypeChoose(BrandApplicationViewModel gettingModel)  // burada yurtiçi yurtdışı application type seçmesi durumunda
        {
            return getBrandAppStandartValues(gettingModel); // burası hep standart
        }
        BrandApplicationViewModel getInformationForBrandApplicationTypeChoose(BrandApplicationViewModel gettingModel)  // burada marka için yapılacak işlemin tipi seçildiğinde yapılacaklar
        {
            BrandApplicationViewModel model = gettingModel;
            model = getBrandAppStandartValues(gettingModel);
            model.prices = _applicationPricesService.getPrices(model.selectedBrandApplication);//standart gelmesi gerek her zaman için 
            model.brandApplicationVisibilty = _mapper.Map<BrandApplicationVisibilityViewModel>(_mainContext.brandApplicationVisiblities.Where(w => w.brandApplicationTypesId == gettingModel.selectedBrandApplication).FirstOrDefault());
            if (model.brandApplicationVisibilty != null && model.brandApplicationVisibilty.applicationClassStatus)
            { // burada eğer  sınıfların gelmesini istiyorsak listeyi çeker
                model.applicationClassList = _brandApplicationClassesService.getApplicationClassList();
                model.applicationSubClassList = _brandApplicationClassesService.getApplicationSubClassList(model.subclassSearchText);
                if (model.subclassSearchText != null)
                {
                    model.applicationClassList=_brandApplicationClassesService.applicationClassFillWithSelecteds(model.applicationClassList, model.applicationSubClassList);
                   // model.prices.totalPrice = _applicationPricesService.brandApplicationTotalPrice(model);
                    double calculatedPrice = _applicationPricesService.brandApplicationTotalPrice(model);
                    model.prices.totalPrice = calculatedPrice;
                    if(model.applicationSubClassList.subList.Where(w=>w.isSelected).Count()==0)
                    {
                        ToastrService.AddToUserQueue(new Toastr("Aradığınız bilgilerde bir sınıf bulunamadı !", type: Model.Enums.ToastrType.Warning));
                    }
                }
                
            }
            if (model.brandApplicationVisibilty != null && model.brandApplicationVisibilty.companiesListStatus)
            {
                model.companieList = _companiesService.GetCompanyList(_usersService.getUserInformation(_cookieService.getSessionEmail()).userId);
            }
            return model;
        }
        BrandApplicationViewModel getInformationForCreateCompanyChoose(BrandApplicationViewModel gettingModel)  // burada firma ekleme yapıldığında dönücek olanlar
        {
            ApplicationClassListViewModel appClass = gettingModel.applicationClassList;
            ApplicationSubClassListViewModel appSubClass = gettingModel.applicationSubClassList;
            BrandApplicationViewModel model = gettingModel;
            model = getBrandAppStandartValues(gettingModel);
            model.prices = _applicationPricesService.getPrices(model.selectedBrandApplication);//standart gelmesi gerek her zaman için 
            model.brandApplicationVisibilty = _mapper.Map<BrandApplicationVisibilityViewModel>(_mainContext.brandApplicationVisiblities.Where(w => w.brandApplicationTypesId == gettingModel.selectedBrandApplication).FirstOrDefault());
            if (model.brandApplicationVisibilty != null && model.brandApplicationVisibilty.applicationClassStatus)
            { // burada eğer  sınıfların gelmesini istiyorsak listeyi çeker

                model.applicationClassList = _brandApplicationClassesService.getApplicationClassList();
                model.applicationSubClassList = _brandApplicationClassesService.getApplicationSubClassList(model.subclassSearchText);
                foreach (var item in model.applicationClassList.applicationClassList)
                {
                    var selectedClass = appClass.applicationClassList.Where(w => w.id == item.id).FirstOrDefault();
                    item.additionalClass = selectedClass.additionalClass;
                    item.isSelected = selectedClass.isSelected;
                }
                foreach (var item in model.applicationSubClassList.subList)
                {
                    var selectedClass = appSubClass.subList.Where(w => w.id == item.id).FirstOrDefault();
                    item.isSelected = selectedClass.isSelected;
                }
                double calculatedPrice = _applicationPricesService.brandApplicationTotalPrice(model);
                model.prices.totalPrice = calculatedPrice;
                

            }
            if (model.brandApplicationVisibilty != null && model.brandApplicationVisibilty.companiesListStatus)
            {
                model.companieList = _companiesService.GetCompanyList(_usersService.getUserInformation(_cookieService.getSessionEmail()).userId);
            }
            return model;
        }
        BrandApplicationViewModel submitBrandApplication(BrandApplicationViewModel gettingModel)
        {
            if (gettingModel.applicationType == ApplicationTypesEnum.domestic && gettingModel.applicationCategoryType == ApplicationCategoryTypesEnum.brand && gettingModel.selectedBrandApplication == 1)
            { // burada marka başvurusu yurtiçi olan
                if (checkSaveBasketPossibilty(gettingModel))
                {
                    if (_basketService.addBasket(gettingModel))
                    {
                        ToastrService.AddToUserQueue(new Toastr("Talebiniz sepete eklenmiştir.", type: Model.Enums.ToastrType.Success));
                        BrandApplicationViewModel returnModel = getBrandAppStandartValues(gettingModel);
                        returnModel.saveStatus = BasketSaveStatusEnum.success;
                        return returnModel;
                    }
                    else
                    {
                        ToastrService.AddToUserQueue(new Toastr("Sepete Eklenirken bir hata gerçekleşti.", type: Model.Enums.ToastrType.Error));
                        return getInformationForBrandApplicationTypeChoose(gettingModel);
                    }
                }
                else
                {
                    
                   return getInformationForBrandApplicationTypeChoose(gettingModel);
                }
            }
            else
            {
                return null;
            }
            
            
            
            
        }
        public bool checkSaveBasketPossibilty(BrandApplicationViewModel Checkmodel)
        {
            bool result = true;
            Checkmodel.brandApplicationVisibilty = _mapper.Map<BrandApplicationVisibilityViewModel>(_mainContext.brandApplicationVisiblities.Where(w => w.brandApplicationTypesId == Checkmodel.selectedBrandApplication).FirstOrDefault());
            if (Checkmodel.brandApplicationVisibilty.applicationClassStatus)
            {
                if(!(Checkmodel.applicationClassList.applicationClassList.Where(w => w.isSelected).Count() > 0 &&
                    Checkmodel.applicationSubClassList.subList.Where(w => w.isSelected).Count() > 0))
                {
                    ToastrService.AddToUserQueue(new Toastr("Lütfen en az 1 sınıf seçiniz.", type: Model.Enums.ToastrType.Error));
                    result = false;
                }
            }
            if (Checkmodel.brandApplicationVisibilty.brandExplanationStatus)
            {
                if (Checkmodel.explanation == "" || Checkmodel.explanation == null)
                {
                    ToastrService.AddToUserQueue(new Toastr("Lütfen açıklama alanını doldurunuz.", type: Model.Enums.ToastrType.Error));
                    result = false;
                }
            }
            if (Checkmodel.brandApplicationVisibilty.brandNameStatus)
            {
                if (Checkmodel.brandName == "" || Checkmodel.brandName == null)
                {
                    ToastrService.AddToUserQueue(new Toastr("Lütfen marka adını doldurunuz.", type: Model.Enums.ToastrType.Error));
                    result = false;
                }
            }
            if (Checkmodel.brandApplicationVisibilty.companiesListStatus)
            {
                if (Checkmodel.selectedCompany== null || Checkmodel.selectedCompany.Count()==0)
                {
                    ToastrService.AddToUserQueue(new Toastr("Lütfen en az 1 adet firma seçiniz.", type: Model.Enums.ToastrType.Error));
                    result = false;
                }
            }
            return result;
        }
       




    }
}