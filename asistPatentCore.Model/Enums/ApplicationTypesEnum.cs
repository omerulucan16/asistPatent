using System;
using System.ComponentModel;

namespace asistPatentCore.Model.Enums
{
    public enum ApplicationTypesEnum
    {
        [Description("Yurtiçi Başvuru")]
        domestic = 0,
        [Description("Yurtdışı Başvuru")]
        international = 1
    }
}
