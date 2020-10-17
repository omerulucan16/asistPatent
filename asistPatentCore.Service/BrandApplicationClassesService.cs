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
    public class BrandApplicationClassesService : IBrandApplicationClassesService
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly ICookieService _cookieService;
        private readonly ICompaniesService _companiesService;
        MainContext _mainContext = new MainContext();
       
        
        public BrandApplicationClassesService(IMapper mapper,IUsersService usersService,ICookieService cookieService,ICompaniesService companiesService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _cookieService = cookieService;
            _companiesService = companiesService;
        }
       
       public ApplicationClassListViewModel getApplicationClassList()
        {
            ApplicationClassListViewModel model = new ApplicationClassListViewModel();
            model.applicationClassList =_mapper.Map<IList<ApplicationClassViewModel>>(_mainContext.applicationClasses.ToList());
            return model;
        }
        public ApplicationSubClassListViewModel getApplicationSubClassList(string searchtext)
        {
            ApplicationSubClassListViewModel model = new ApplicationSubClassListViewModel();
            model.subList = _mapper.Map<IList<ApplicationSubClassViewModel>>(_mainContext.applicationSubClasses.Select(s => new ApplicationSubClassViewModel()
            {
                appclassId = s.appclassId,
                appclassName = s.ApplicationClass.appclassname,
                id = s.id,
                isSelected = false,
                status = s.status,
                subclasscode = s.subclasscode,
                subclassname = s.subclassname
            }).ToList());
            if (searchtext != null )
            {
                var search = model.subList.Where(w => w.appclassName.Contains(searchtext) || w.subclassname.Contains(searchtext)).ToList();
                var emtiaSearch = searchApplicationClassEmtiaList(searchtext);
                foreach (var item in model.subList)
                {
                    if (search.Where(w=>w.id ==item.id).Count()>0 || emtiaSearch.emtiaList.Where(w=>w.appSubClassId == item.id).Count()>0)
                    {
                        item.isSelected = true;

                    }
                }
            }

            return model;
        }
        public ApplicationClassListViewModel applicationClassFillWithSelecteds(ApplicationClassListViewModel mainClass, ApplicationSubClassListViewModel subclass)
        {
            foreach (var item in mainClass.applicationClassList)
            {
                
                if (subclass.subList.Where(w=> w.isSelected == true && w.appclassId == item.id).Count()>0)
                {
                    item.isSelected = true;
                }
            }
            return mainClass;
        }
        ApplicationEmtiaClassListViewModel searchApplicationClassEmtiaList(string searchText)
        {
            ApplicationEmtiaClassListViewModel model = new ApplicationEmtiaClassListViewModel();
            model.emtiaList = _mapper.Map<IList<ApplicationEmtiaClassViewModel>>(_mainContext.applicationEmtiaClasses.Where(w=>w.value.Contains(searchText)).Select(s => new ApplicationEmtiaClassViewModel()
            {
                appSubClassId = s.ApplicationSubClass.id,
                id = s.id,
                status = s.status,
                value=s.value
            }).ToList());
            return model;
        }
    }
}