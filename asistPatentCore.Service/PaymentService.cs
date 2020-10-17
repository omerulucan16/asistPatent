using System;
using AutoMapper;
using System.Linq;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Data;
using asistPatentCore.ViewModel;
using asistPatentCore.Model;
using asistPatentCore.Model.Enums;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Xml;
using System.Net;

namespace asistPatentCore.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUsersService _usersService;
        private readonly ICookieService _cookieService;
        private readonly IUploadImageService _uploadImageService;
        private readonly ICompaniesService _companiesService;
        private readonly IApplicationPricesService _applicationPricesService;
        private readonly IDefaultValuesService _defaultValuesService;
        private readonly IBasketService _basketService;
        private readonly ICountersService _countersService;
        private readonly IAgreementService _agreementService;
        private readonly HttpContext _httpContext;
        MainContext _mainContext = new MainContext();

        public PaymentService(IMapper mapper,IEmailService emailService,IUsersService usersService,ICookieService cookieService, IUploadImageService uploadImageService,IApplicationPricesService applicationPricesService,IBasketService basketService,IDefaultValuesService defaultValuesService,ICountersService countersService,HttpContext httpContext,IAgreementService agreementService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _emailService = emailService;
            _cookieService = cookieService;
            _uploadImageService = uploadImageService;
            _applicationPricesService = applicationPricesService;
            _basketService = basketService;
            _defaultValuesService = defaultValuesService;
            _countersService = countersService;
            _httpContext = httpContext;
            _agreementService = agreementService;
        }

        public PaymentViewModel getPaymentInformation()
        {
            PaymentViewModel model = new PaymentViewModel();
            Guid userId = _usersService.getUserInformation(_cookieService.getSessionEmail()).userId;
            model.agreementMessage = _defaultValuesService.getDefaultValue("satissozlesmesi");
            int activeYear = DateTime.Now.Year;
            List<int> yearList = new List<int>();
            yearList.Add(activeYear);
            for (int i = 1; i < 10; i++)
            {
                yearList.Add(activeYear + i);
            }
            model.creditCardYearList = yearList.Select(s => new SelectListItem()
            {
                Text = s.ToString(),
                Value=s.ToString()
            }).ToList();
            model.companiesForInvoice = _mainContext.companies.Where(w => w.userId == userId).Select(s => new SelectListItem()
            {
                Text = s.companyTitle + " - " + s.adress,
                Value = s.id.ToString()
            }).ToList();
            model.paymentStatus = PaymentResultsStatusEnum.none;
            model.creditCardMonthList = ((CreditCardMonthEnum[])Enum.GetValues(typeof(CreditCardMonthEnum))).Select(c => new SelectListItem() { Value = ((int)c).ToString(), Text = c.GetDescription() }).ToList();
            var myBasket = _basketService.myBasketList();
            model.totalPrice = myBasket.basketList.Sum(i=>i.totalPrice);
            model.totalPriceWithoutTax = myBasket.basketList.Sum(i=>i.priceWithoutTax);
            model.bankAccounts = new BankAccountsListViewModel();
            model.bankAccounts.bankAccountsList= _mapper.Map<IList<BankAccountsViewModel>>(_mainContext.bankAccounts.Where(w => w.status == GeneralStatusEnum.Active).ToList());
            return model;
        }
        public PaymentViewModel createAgrement(PaymentViewModel paymentModel)
        {
            if (paymentModel.isAgreement)
            {
                if (paymentModel.creditoreft == "0")
                {// havale eft
                    paymentModel.paymentStatus = PaymentResultsStatusEnum.eftprocessdone;
                    paymentModel.basketGroupId = _countersService.getNewCounter(CounterTypeEnum.agreementCounter);
                    if(makeGroupIdBasketndAgreement(paymentModel))
                    {
                        paymentModel.eftdoneMessage = _defaultValuesService.getDefaultValue("efttamamlandi");
                        paymentModel.bankAccounts = new BankAccountsListViewModel();
                        paymentModel.bankAccounts.bankAccountsList = _mapper.Map<IList<BankAccountsViewModel>>(_mainContext.bankAccounts.Where(w => w.status == GeneralStatusEnum.Active).ToList());
                        paymentModel.paymentStatus = PaymentResultsStatusEnum.eftprocessdone;
                    }
                    else
                    {
                        paymentModel = getPaymentInformation();
                        ToastrService.AddToUserQueue(new Toastr("İşlem sırasında bir hata oluştu. Lütfen tekrar deneyiniz", type: Model.Enums.ToastrType.Error));
                    }
                }
                else
                {
                    payOnlineCreditCard(paymentModel);
                }

            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Lütfen satış sözleşmesini onaylayınız.", type: Model.Enums.ToastrType.Error));

            }

            return paymentModel;
        }
        bool makeGroupIdBasketndAgreement(PaymentViewModel model)
        {
            
            IList<Basket> basketList = _mainContext.baskets.Where(w => w.basketStatus == BasketStatusEnum.active && w.userId == _usersService.getUserInformation(_cookieService.getSessionEmail()).userId).ToList();
            Guid responsibleUser = new Guid();
            responsibleUser = _usersService.getUserInformation(_cookieService.getSessionEmail()).responsibleUserId;
           
            foreach (var item in basketList)
            {
                Guid agreementsId = Guid.NewGuid();
                int firstStatus = _agreementService.getFirstStatus();
                item.basketGroupId = model.basketGroupId;
                item.basketStatus = BasketStatusEnum.done;
                Agreements agreementsModel = new Agreements();
                agreementsModel.id = agreementsId;
                agreementsModel.appCategoryType = ApplicationCategoryTypesEnum.brand;
                agreementsModel.basketId = item.basketId;
                agreementsModel.groupId = model.basketGroupId;
                agreementsModel.companyIdForInvoice = Convert.ToInt32(model.selectedCompanieForInvoice);
                agreementsModel.statusId = firstStatus;
                agreementsModel.generalStatus = GeneralStatusEnum.Active;
                agreementsModel.paymentType = model.paymentStatus;
                agreementsModel.processDate = DateTime.Now;
                if(responsibleUser != new Guid())
                {
                    agreementsModel.responsibleUserId = responsibleUser;
                }
                
                AgreementsChanges agreementsChangesModel = new AgreementsChanges();
                agreementsChangesModel.agreementId = agreementsId;
                agreementsChangesModel.comments = "";
                agreementsChangesModel.generalStatus = GeneralStatusEnum.Active;
                agreementsChangesModel.processDate = DateTime.Now;
                agreementsChangesModel.status = firstStatus;
                agreementsChangesModel.userId = _usersService.getUserInformation(_cookieService.getSessionEmail()).userId;
                _mainContext.Add(agreementsChangesModel);
                _mainContext.Add(agreementsModel);
            }
            if(_mainContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        bool payOnlineCreditCard(PaymentViewModel model)
        {
            var myBasket = _basketService.myBasketList();
            model.totalPrice = myBasket.basketList.Sum(i => i.totalPrice);
            var basketGroupId = Guid.NewGuid();
            string strMode = "PROV";
            string strVersion = "v0.01";
            string strTerminalID = "10015840"; //8 Haneli TerminalID yazılmalı.
            string _strTerminalID = "0" + strTerminalID;
            string strProvUserID = "PROVAUT"; //TerminalProvUserID kullanıcısı
            string strProvisionPassword = "Kahb12)4"; //TerminalProvUserID şifresi
            string strUserID = "AsistPatent"; // Kullanıcı ID'si
            string strMerchantID = "1639003"; //Üye İşyeri Numarası
            string strEmailAddress = _cookieService.getSessionEmail(); // Müşterinin e-postası
            string strIPAddress = "192.168.1.2";//_httpContext.Connection.RemoteIpAddress.ToString();
            string strOrderID = ""; // Sipariş ID'si
            string strNumber = model.creditcardNumber; // Kredi kartı numarası
            string selectedMonth =((int)model.selectedCardMonth).ToString().Length< 2 ? "0" + ((int)model.selectedCardMonth).ToString() : ((int)model.selectedCardMonth).ToString();
            string selectedYear = model.selectedCardYear.ToString().Substring(2, 2);
            string strExpireDate = selectedMonth + selectedYear; //Kart son kullanma tarihi. Mart 2016 için 0316 şeklinde yazmalı
            string strCVV2 = model.selectedCVV.ToString();
            string strAmount = "100";
            string strType = "sales";
            string strCurrencyCode = "949"; // 949 = Türk Lirası
            string strCardholderPresentCode = "0";
            string strMotoInd = "N";
            string strInstallmentCount = "";
            string strHostAddress = "https://sanalposprov.garanti.com.tr/VPServlet";
            string SecurityData = GetSHA1(strProvisionPassword + _strTerminalID).ToUpper();
            string HashData = GetSHA1(strOrderID + strTerminalID + strNumber + strAmount + SecurityData).ToUpper();
            string strXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<GVPSRequest>" + "<Mode>" + strMode + "</Mode>" + "<Version>" + strVersion + "</Version>" + "<Terminal><ProvUserID>" + strProvUserID + "</ProvUserID><HashData>" + HashData + "</HashData><UserID>" + strUserID + "</UserID><ID>" + strTerminalID + "</ID><MerchantID>" + strMerchantID + "</MerchantID></Terminal>" + "<Customer><IPAddress>" + strIPAddress + "</IPAddress><EmailAddress>" + strEmailAddress + "</EmailAddress></Customer>" + "<Card><Number>" + strNumber + "</Number><ExpireDate>" + strExpireDate + "</ExpireDate><CVV2>" + strCVV2 + "</CVV2></Card>" + "<Order><OrderID>" + strOrderID + "</OrderID><GroupID></GroupID><AddressList><Address><Type>S</Type><Name></Name><LastName></LastName><Company></Company><Text></Text><District></District><City></City><PostalCode></PostalCode><Country></Country><PhoneNumber></PhoneNumber></Address></AddressList></Order>" + "<Transaction>" + "<Type>" + strType + "</Type><InstallmentCnt>" + strInstallmentCount + "</InstallmentCnt><Amount>" + strAmount + "</Amount><CurrencyCode>" + strCurrencyCode + "</CurrencyCode><CardholderPresentCode>" + strCardholderPresentCode + "</CardholderPresentCode><MotoInd>" + strMotoInd + "</MotoInd>" + "</Transaction>" + "</GVPSRequest>";
            string result = pay(strXML, strHostAddress);
            return true;
        }
        string pay(string strXml, string strHostAdress)
        {
            try
            {
                string data = "data=" + strXml;

                WebRequest _WebRequest = WebRequest.Create(strHostAdress);
                _WebRequest.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                _WebRequest.ContentType = "application/x-www-form-urlencoded";
                _WebRequest.ContentLength = byteArray.Length;

                Stream dataStream = _WebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse _WebResponse = _WebRequest.GetResponse();
                //Console.WriteLine(((HttpWebResponse)_WebResponse).StatusDescription);
                dataStream = _WebResponse.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                //Müşteriye gösterilebilir ama Fraud riski açısından bu değerleri göstermeyelim.
                //Console.WriteLine(responseFromServer);

                //GVPSResponse XML'in değerlerini okuyoruz. İstediğiniz geri dönüş değerlerini gösterebilirsiniz.
                string XML = responseFromServer;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(XML);

                //ReasonCode
                XmlElement xElement1 = xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/ReasonCode") as XmlElement;
                //lblResult2.Text = xElement1.InnerText;

                //Message
                //XmlElement xElement2 = xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/Message") as XmlElement;
                //lblResult2.Text = xElement2.InnerText;

                //ErrorMsg
                XmlElement xElement3 = xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/ErrorMsg") as XmlElement;

                //00 ReasonCode döndüğünde işlem başarılıdır. Müşteriye başarılı veya başarısız şeklinde göstermeniz tavsiye edilir. (Fraud riski)
                

                if (xElement1.InnerText == "00")
                {
                    return "Ödemeniz başarıyla gerçekleşti. Siparişiniz derhal hazırlanıp gönderilecektir.<br />";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string GetSHA1(string text)
        {
            var cryptoServiceProvider = new SHA1CryptoServiceProvider();
            var inputbytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(text));

            var builder = new StringBuilder();
            for (int i = 0; i < inputbytes.Length; i++)
            {
                builder.Append(string.Format("{0,2:x}", inputbytes[i]).Replace(" ", "0"));
            }

            return builder.ToString().ToUpper();
        }

      
       

        


    }
}