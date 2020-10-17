using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class AgreementsRoles
    {
        public int id { get; set; }


        public Guid userId { get; set; }
        public virtual Users Users { get; set; }


        public int agreementsStatusesId { get; set; }
        public virtual AgreementStatuses AgreementStatuses { get; set; }
    }
}
