using System;
using System.ComponentModel;

namespace asistPatentCore.Model.Enums
{
    public enum BankAccountTypeEnum
    {
        [Description("Türk Lirası")]
        tl = 0,
        [Description("Amerikan Doları")]
        usd = 1,
        [Description("Euro")]
        euro=2
    }
}
