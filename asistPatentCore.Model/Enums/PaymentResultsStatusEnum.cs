using System;
using System.ComponentModel;

namespace asistPatentCore.Model.Enums
{
    public enum PaymentResultsStatusEnum
    {
        [Description("İşlem yapılmadı")]
        none = 0,
        [Description("Havale/Eft yapıldı")]
        eftprocessdone = 1,
        [Description("Kredi Kartı Ödeme yapıldı")]
        creditcarddone = 2
    }
}
