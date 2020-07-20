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

namespace asistPatentCore.Service
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        MainContext _mainContext = new MainContext();
       
        private IMapper mapper;

        public UsersService(IMapper mapper,IEmailService emailService)
        {

            _mapper = mapper;
            _emailService = emailService;
           
        }

       
        public UsersListViewModel getUsersList()
        {
            UsersListViewModel model = new UsersListViewModel();
            model.userList = _mapper.Map<IList<UsersViewModel>>(_mainContext.users.ToList());
            return model;
        }
        public bool checkEmailAdress(string email)
        {
            

            if (email == null)
                return false;
            else
            {
                if (email.Length < 7)
                    return false;
                else
                {
                    if (!email.Contains("@") || !email.Contains("."))
                        return false;
                    //else
                        //if (_mainContext.users.Where(w => w.userEmailAdress == email).Count() > 0)
                        //return false;
                    else
                        return true;

                }
            }
        }
        public bool checkEmailAdressCreate(string email)
        {


            if (email == null)
                return false;
            else
            {
                if (email.Length < 7)
                    return false;
                else
                {
                    if (!email.Contains("@") || !email.Contains("."))
                        return false;
                    else
                        if (!checkUserHave(email))
                        return false;
                    else
                        return true;

                }
            }
        }
        public bool checkUserPassword(string password)
        {
            if (password == null)
                return false;
            else if (password.Length < 6 || password == null)
                return false;
            else
            {
                if (password.Contains("=") || password.Contains("'"))
                    return false;
                else
                    return true;
            }
        }
        public bool checkUserNameSurname(string name, string surname)
        {
            if (name == null || surname == null)
                return false;
            else if (name.Length < 2 || surname.Length < 2)
                return false;
            else
                return true;
        }
        public bool checkPhoneNumber(string phone)
        {
            
            if (phone != null)
            {
                
                phone = Regex.Replace(phone, @"[^0-9]+", "");
                
                if (phone[0].ToString() == "0")
                {
                    return false;
                }
                else if(phone[0].ToString() == "5" && phone.Length==10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        public bool checkUserHave(string email)
        {
            if (_mainContext.users.Where(w => w.userEmailAdress == email).Count() == 0)
                return true;
            else
                return false;
            
        }
        public UsersViewModel getUserInformation(string email)
        {
            UsersViewModel model = new UsersViewModel();
            model = _mapper.Map<UsersViewModel>(_mainContext.users.Where(w => w.userEmailAdress == email).FirstOrDefault());
            model.userPassword = "*";
            return model;
        }
        public UsersViewModel getUserInformationFromId(Guid userId)
        {
            UsersViewModel model = new UsersViewModel();
            model = _mapper.Map<UsersViewModel>(_mainContext.users.Where(w => w.userId == userId).FirstOrDefault());
            model.userPassword = "*";
            return model;
        }
        public string getUserIdFromUserToken(Guid userTokenId)
        {

            var userToken = _mainContext.usersTokens.Where(w => w.tokenId == userTokenId && w.status == UsersTokenEnum.Active).FirstOrDefault();
            if (userToken != null)
            {
                return userToken.userId.ToString();
            }
            else
            {
                return null;
            }
        }
        public UsersViewModel createSocialUser(UsersViewModel socialModel)
        {
            Users model = new Users();
            model = _mapper.Map<Users>(socialModel);
            model.role = Model.Enums.UserRoleEnum.customer;
            model.status = Model.Enums.UserStatusEnum.Active;
            model.userCreateDate = DateTime.Now;
            _mainContext.Add(model);
            _mainContext.SaveChanges();
            return _mapper.Map<UsersViewModel>(model);

        }
        public bool registerUser(RegisterViewModel registerModel)
        {
            if (createUser(registerModel))
            {
                UsersViewModel registeredUserInformation = getUserInformation(registerModel.userEmailAdress);
                if (createUserToken(UsersTokenTypeEnum.register, registeredUserInformation.userId) != null)
                {
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        public bool createUser(RegisterViewModel registerModel)
        {
            if ((registerModel.userPassword != registerModel.userRePassword) || !checkEmailAdressCreate(registerModel.userEmailAdress)
                || !checkUserNameSurname(registerModel.userName,registerModel.userSurname)
                || !checkUserPassword(registerModel.userPassword) || !checkPhoneNumber(registerModel.userPhoneNumber))
            {
                ToastrService.AddToUserQueue(new Toastr("Kayıt sırasında bir hata oluştu.", type: Model.Enums.ToastrType.Error));
                return false;

            }
            else
            {
                Users model = new Users();
                model = _mapper.Map<Users>(registerModel);
                model.userPhoneNumber = Regex.Replace(model.userPhoneNumber, @"[^0-9]+", "");
                model.role = Model.Enums.UserRoleEnum.customer;
                model.status = Model.Enums.UserStatusEnum.Waiting;
                model.userCreateDate = DateTime.Now;
                _mainContext.Add(model);
                if (_mainContext.SaveChanges() == 1)

                    return true;
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Kayıt sırasında bir hata oluştu.", type: Model.Enums.ToastrType.Error));
                    return false;
                }
                
            }
        }
        public string createUserToken(UsersTokenTypeEnum tokenType,Guid userid)
        {
            UsersToken usersTokenCreate = new UsersToken();
            usersTokenCreate.createDate = DateTime.Now;
            usersTokenCreate.status = UsersTokenEnum.Active;
            usersTokenCreate.userId = userid;
            usersTokenCreate.type = tokenType;
            _mainContext.usersTokens.Add(usersTokenCreate);
            if (_mainContext.SaveChanges() == 1)
            {
                return usersTokenCreate.tokenId.ToString();
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Kayıt sırasında bir hata oluştu.", type: Model.Enums.ToastrType.Error));
                return null;
            }
        }
        public bool forgetPassSend(string emailaddress)
        {
            UsersViewModel forgetUserInformation = getUserInformation(emailaddress);
            string userToken = createUserToken(UsersTokenTypeEnum.forgetpass, forgetUserInformation.userId);
            if (userToken != null )
            {
                forgetUserInformation.accessToken = userToken;
                if (_emailService.sendEmail(_emailService.sendForgetPassEmail(forgetUserInformation)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public string getUserToken(UsersTokenTypeEnum tokenType, Guid userid)
        {
            UsersToken usersTokenModel = _mainContext.usersTokens.Where(w => w.status == UsersTokenEnum.Active && w.userId == userid && w.type == tokenType).OrderByDescending(o => o.createDate).FirstOrDefault();
            if (usersTokenModel != null && usersTokenModel.tokenId != null )
            {
                return usersTokenModel.tokenId.ToString();
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Kayıt sırasında bir hata oluştu.", type: Model.Enums.ToastrType.Error));
                return null;
            }
        }
        public void checkRegisterToken(Guid tokenid)
        {
            var userToken = _mainContext.usersTokens.Where(w => w.tokenId == tokenid && w.status == UsersTokenEnum.Active).FirstOrDefault();
            if (userToken != null)
            {
                Guid getUserId = userToken.userId;
                if (getUserToken(UsersTokenTypeEnum.register, getUserId) == tokenid.ToString())
                {
                    if (changeTokenStatus(tokenid) && changeRegisterUserStatus(getUserId))
                    {
                        ToastrService.AddToUserQueue(new Toastr("Hesabınız başarıyla aktif edilmiştir.", type: Model.Enums.ToastrType.Success));

                    }
                    else
                    {
                        ToastrService.AddToUserQueue(new Toastr("Hesabınız aktif edilirken bir hata gerçekleşmiştir.", type: Model.Enums.ToastrType.Error));

                    }

                }
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Hesabınız aktif edilirken bir hata gerçekleşmiştir.", type: Model.Enums.ToastrType.Error));

                }
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Hesabınız aktif edilirken bir hata gerçekleşmiştir.", type: Model.Enums.ToastrType.Error));

            }

        }
        public bool changeTokenStatus(Guid tokenId)
        {
            UsersToken usersToken = _mainContext.usersTokens.Where(w => w.tokenId == tokenId).FirstOrDefault();
            usersToken.status = UsersTokenEnum.Passive;
            if (_mainContext.SaveChanges() == 1)
                return true;
            else
                return false;
            
            
        }
        public bool changeRegisterUserStatus(Guid userid)
        {
            Users userStatus  = _mainContext.users.Where(w => w.userId == userid).FirstOrDefault();
            userStatus.status = UserStatusEnum.Active;
            if (_mainContext.SaveChanges() == 1)
                return true;
            else
                return false;


        }
        public bool changeRegisterUserPasswordWithEmail(MyAccountViewModel model)
        {
            Users userInformation = _mainContext.users.Where(w => w.userEmailAdress == model.userEmailAdress && w.status == UserStatusEnum.Active).FirstOrDefault();
            if (userInformation != null && model.userPassword == model.userRePassword && model.userActivePassword == userInformation.userPassword && checkUserPassword(model.userPassword))
            {
                userInformation.userPassword = model.userPassword;
                if (_mainContext.SaveChanges() == 1)
                {
                    ToastrService.AddToUserQueue(new Toastr("Şifreniz başarıyla değiştirilmiştir.", type: Model.Enums.ToastrType.Success));
                    return true;
                }
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Şifreniz değiştirilirken bir hata ile karşılaşıldı.", type: Model.Enums.ToastrType.Error));
                    return false;
                }
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Şifreniz değiştirilirken bir hata ile karşılaşıldı.", type: Model.Enums.ToastrType.Error));
                return false;
            }
        }

        public bool changeRegisterUserPassword(RegisterViewModel model)
        {
            string userId = getUserIdFromUserToken(new Guid(model.accessToken));
            if (userId != null && (model.userPassword == model.userRePassword) && checkUserPassword(model.userPassword))
            {
                Users userStatus = _mainContext.users.Where(w => w.userId == new Guid(userId)).FirstOrDefault();
                userStatus.userPassword = model.userPassword;
                if (_mainContext.SaveChanges() == 1)
                {
                    ToastrService.AddToUserQueue(new Toastr("Şifreniz başarıyla sıfırlanmıştır.", type: Model.Enums.ToastrType.Success));
                    changeTokenStatus(new Guid(model.accessToken));
                    return true;
                }
                
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Şifreniz sıfırlanırken bir hata ile karşılaşıldı.Parolanıza bir öncekiyle aynı olmamalıdır.", type: Model.Enums.ToastrType.Error));
                    return false;
                }
                
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Şifreniz sıfırlanırken bir hata ile karşılaşıldı. Lütfen parolanızı kontrol ediniz.", type: Model.Enums.ToastrType.Error));
                return false;
            }





        }
        public bool checkForgetPassToken(Guid tokenId)
        {
            var userToken = _mainContext.usersTokens.Where(w => w.tokenId == tokenId && w.status == UsersTokenEnum.Active).FirstOrDefault();
            if (userToken != null)
            {
                Guid getUserId = userToken.userId;
                if (getUserToken(UsersTokenTypeEnum.forgetpass, getUserId) == tokenId.ToString())
                {

                    return true;
                }
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Şifre sıfırlama talebiniz zaman aşımına uğramıştır.", type: Model.Enums.ToastrType.Error));
                    return false;
                }
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Şifre sıfırlama talebiniz zaman aşımına uğramıştır.", type: Model.Enums.ToastrType.Error));
                return false;
            }
        }
        public bool loginUser(string email, string password)
        {
            Users model = _mainContext.users.Where(w =>  w.status == Model.Enums.UserStatusEnum.Active && w.userEmailAdress == email).FirstOrDefault();
            if ( model != null &&  model.userEmailAdress != null && model.userEmailAdress != "")
            {
                if (model.userPassword == "")
                {
                    ToastrService.AddToUserQueue(new Toastr("Hatalı giriş", type: Model.Enums.ToastrType.Error));
                    return false;
                }
                else
                {
                    if (checkEmailAdress(model.userEmailAdress) && checkUserPassword(model.userPassword))
                    {
                        if (model.userPassword == password)
                        {
                            ToastrService.AddToUserQueue(new Toastr("Tebrikler , başarıyla giriş yaptınız", type: Model.Enums.ToastrType.Success));
                            return true;
                        }
                        else
                        {
                            ToastrService.AddToUserQueue(new Toastr("Hatalı giriş", type: Model.Enums.ToastrType.Error));
                            return false;
                        }
                    }
                    else
                    {
                        ToastrService.AddToUserQueue(new Toastr("Hatalı giriş", type: Model.Enums.ToastrType.Error));
                        return false;
                    }
                    
                        
                }
                
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Hatalı giriş", type: Model.Enums.ToastrType.Error));
                return false;
            }
        }
    }
}