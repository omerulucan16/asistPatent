using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class UsersToken 
    {
        public Guid tokenId { get; set; } = Guid.NewGuid();
        public DateTime createDate { get; set; }
        public UsersTokenTypeEnum type { get; set; }
        public Guid userId { get; set; }
        public virtual Users Users { get; set; }
        public UsersTokenEnum status { get; set; }
    }
}
