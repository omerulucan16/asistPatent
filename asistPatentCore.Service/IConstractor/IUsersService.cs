using System;
using asistPatentCore.Data;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using asistPatentCore.ViewModel;
namespace asistPatentCore.Service.IConstractor
{
    public interface IUsersService
    {

         bool checkEmailAdress(string email);
         bool checkUserPassword(string password);
         bool checkUserNameSurname(string name, string surname);
         bool checkPhoneNumber(string phone);
         bool checkUserHave(string email);
         UsersViewModel getUserInformation(string email);
        UsersViewModel createSocialUser(UsersViewModel socialModel);
        bool loginUser(string email, string password);
        bool checkEmailAdressCreate(string email);
        bool registerUser(RegisterViewModel registerModel);
        bool createUser(RegisterViewModel registerModel);
        string createUserToken(UsersTokenTypeEnum tokenType, Guid userid);
        string getUserToken(UsersTokenTypeEnum tokenType, Guid userid);
       void checkRegisterToken(Guid tokenid);
        bool changeRegisterTokenStatus(Guid tokenId);
        bool changeRegisterUserStatus(Guid userid);
    }
}
