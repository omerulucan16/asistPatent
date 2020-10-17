using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class BankAccountsViewModel
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
    public class BankAccountsListViewModel
    {
        public IList<BankAccountsViewModel> bankAccountsList { get; set; }
    }

}
