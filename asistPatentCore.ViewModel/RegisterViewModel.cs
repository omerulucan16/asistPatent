using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.ViewModel
{
    public class RegisterViewModel
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string userEmailAdress { get; set; }
        public string userPassword { get; set; }
        public string userRePassword { get; set; }
        public string userPhoneNumber { get; set; }
        public string accessToken { get; set; }
        public bool isRegistered { get; set; } = false;
       
        public bool termscondition { get; set; }
        public string termsconditionMessage { get; set; }
        public UserStatusEnum status { get; set; }
        public ProviderEnum provider { get; set; }
        public DateTime userCreateDate { get; set; }
    }
}
