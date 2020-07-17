using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class Users
    {
        public Guid userId { get; set; } = Guid.NewGuid();
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string userEmailAdress { get; set; }
        public string userPassword { get; set; }
        public string userPhoneNumber { get; set; }
        public UserStatusEnum status { get; set; }
        public UserRoleEnum role { get; set; }
        public DateTime userCreateDate { get; set; }
        public virtual ICollection<UsersToken> UsersTokenRelation { get; set; }

    }
}
