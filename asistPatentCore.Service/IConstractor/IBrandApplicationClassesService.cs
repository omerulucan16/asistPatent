using System;
using asistPatentCore.Data;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface IBrandApplicationClassesService
    {
          ApplicationClassListViewModel getApplicationClassList();
          ApplicationSubClassListViewModel getApplicationSubClassList(string search);
          ApplicationClassListViewModel applicationClassFillWithSelecteds(ApplicationClassListViewModel mainClass, ApplicationSubClassListViewModel subclass);

    }
}
