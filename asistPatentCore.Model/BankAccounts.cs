using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;

namespace asistPatentCore.Model
{
    public class BankAccounts
    {
        public int id { get; set; }
        public string bankName { get; set; }
        public BankAccountTypeEnum accountCurrencyType { get; set; }
        public string brunchName { get; set; }
        public int brunchCode { get; set; }
        public int accountNumber { get; set; }
        public string ibanNo { get; set; }
        public GeneralStatusEnum status { get; set; }
    }
}
