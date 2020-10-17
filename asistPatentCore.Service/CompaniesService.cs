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
    public class CompaniesService : ICompaniesService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUsersService _usersService;
        private readonly ICookieService _cookieService;
        MainContext _mainContext = new MainContext();

        public CompaniesService(IMapper mapper,IEmailService emailService,IUsersService usersService,ICookieService cookieService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _emailService = emailService;
            _cookieService = cookieService;


        }

        public IEnumerable<SelectListItem> GetCompanyList(Guid userId)
        {
            
            return _mainContext.companies.Where(w => w.userId == userId).Select(s=>new SelectListItem()
            {
                Text=s.companyTitle,
                 Value=s.id.ToString()
            }).ToList();
        }
      
        public bool createCompany(CompaniesViewModel model)
        {
            if (!checkCreateCompany(model))
            {
                return false;
            }
            Companies createModel = new Companies();
            createModel = _mapper.Map<Companies>(model);
            if (model.companyTypeChoose == "0")
            {
                createModel.companyType = CompaniesTypeEnum.personel;
            }
            else
            {
                createModel.companyType = CompaniesTypeEnum.lls;
            }
            createModel.userId = _usersService.getUserInformation(_cookieService.getSessionEmail()).userId;
            _mainContext.companies.Add(createModel);
            if (_mainContext.SaveChanges() > 0)

                return true;
            else
                return false;
        }

        bool checkCreateCompany(CompaniesViewModel model)
        {
            if (model.adress == null  || model.companyTitle == null )
            {
                ToastrService.AddToUserQueue(new Toastr("Lütfen adres ve şirket adınızı boş geçmeyiniz.", type: Model.Enums.ToastrType.Warning));
                return false;
                
            }
            else
            {
                if (model.companyType == CompaniesTypeEnum.personel)
                {
                    if (model.identyNumber.ToString() == null)
                    {
                        ToastrService.AddToUserQueue(new Toastr("Lütfen Tc Kimlik numaranızı boş geçmeyiniz.", type: Model.Enums.ToastrType.Warning));
                        return false;
                    }
                    else
                    {
                        ToastrService.AddToUserQueue(new Toastr("Başarıyla şirket eklediniz.", type: Model.Enums.ToastrType.Success));
                        return true;
                    }
                }
                else
                {
                    if (model.taxCenter == null || model.taxNumber.ToString() == null)
                    {
                        ToastrService.AddToUserQueue(new Toastr("Lütfen Vergi Dairesi ve Vergi Numaranızı boş geçmeyiniz.", type: Model.Enums.ToastrType.Warning));
                        return false;
                    }
                    else
                    {
                        ToastrService.AddToUserQueue(new Toastr("Başarıyla şirket eklediniz.", type: Model.Enums.ToastrType.Success));
                        return true;
                    }
                }
            }
        }


    }
}