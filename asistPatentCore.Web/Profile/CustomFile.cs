using System;
using AutoMapper;
using asistPatentCore.Model;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Web.Profile
{
    public class CustomProfile : AutoMapper.Profile
    {
        public CustomProfile()
        {
            CreateMap<UsersViewModel, Users>().ReverseMap();
            CreateMap<RegisterViewModel, Users>().ReverseMap();
            CreateMap<UsersViewModel, RegisterViewModel>().ReverseMap();
            CreateMap<EmailViewModel, UsersViewModel>().ReverseMap();
            CreateMap<EmailViewModel, MailTemplates>().ReverseMap();
        }
    }
}
