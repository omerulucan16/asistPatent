using System;
using System.Collections.Generic;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asistPatentCore.ViewModel
{
    public class PaymentViewModel
    {
        public IEnumerable<SelectListItem> companiesForInvoice { get; set; }
        public IEnumerable<SelectListItem> creditCardMonthList { get; set; }
        public IEnumerable<SelectListItem> creditCardYearList { get; set; }
        public CreditCardMonthEnum selectedCardMonth { get; set; }
        public int selectedCardYear { get; set; }
        public string creditcardNumber { get; set; }
        public string creditcardName { get; set; }
        public string eftdoneMessage { get; set; }
        public int selectedCVV { get; set; }
        public int basketGroupId { get; set; }
        public PaymentResultsStatusEnum paymentStatus { get; set; }
        public BankAccountsListViewModel bankAccounts { get; set; }
        public string selectedCompanieForInvoice { get; set; }
        public double totalPrice { get; set; }
        public string agreementMessage { get; set; }
        public bool isAgreement { get; set; }
        public double totalPriceWithoutTax { get; set; }
        public string creditoreft { get; set; }

    } 
    
}
