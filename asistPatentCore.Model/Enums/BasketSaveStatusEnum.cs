using System;
using System.ComponentModel;

namespace asistPatentCore.Model.Enums
{
    public enum BasketSaveStatusEnum
    {
        [Description("Bekliyor")]
        waiting = 0,
        [Description("Kaydedildi")]
        success = 1
    }
}
