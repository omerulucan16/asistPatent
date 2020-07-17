using System;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class DefaultValues
    {
        public int id { get; set; }
        public string key { get; set; }
        public DefaultValuesEnum status { get; set; }
        public string value { get; set; }

    }
}
