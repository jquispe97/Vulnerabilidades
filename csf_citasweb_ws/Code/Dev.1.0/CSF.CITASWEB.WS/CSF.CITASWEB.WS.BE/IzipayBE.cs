using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSF.CITASWEB.WS.BE
{
    public class IzipayBE
    {

    }
    public class IpnBE
    {
        //public string kr-answer { get; set; }
        
    }
    public class IPNIzipayBE
    {
        public string code { get; set; }
        public string message { get; set; }
        public string messageUser { get; set; }
        public string messageUserEng { get; set; }
        public IIResponseBE response { get; set; }
        public string payloadHttp { get; set; }
        public string signature { get; set; }
        public string transactionId { get; set; }
    }
    public class IIResponseBE
    {
        public string payMethod { get; set; }
        public List<IIOrderBE> order { get; set; }
        public IICardBE card { get; set; }
        public IIBillingBE billing { get; set; }
        public IIMerchantBE merchant { get; set; }
        public IITokenBE token { get; set; }
        public IIAuthenticationBE authentication { get; set; }
        public string[] customFields { get; set; }
    }
    public class IIOrderBE
    {
        public string payMethodAuthorization { get; set; }
        public string codeAuth { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string installment { get; set; }
        public string deferred { get; set; }
        public string orderNumber { get; set; }
        public string stateMessage { get; set; }
        public string dateTransaction { get; set; }
        public string timeTransaction { get; set; }
        public string uniqueId { get; set; }
        public string referenceNumber { get; set; }
    }
    public class IICardBE
    {
        public string brand { get; set; }
        public string pan { get; set; }
        public string save { get; set; }
    }
    public class IIBillingBE
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string documentType { get; set; }
        public string document { get; set; }
        public string companyName { get; set; }
    }
    public class IIMerchantBE
    {
        public string merchantCode { get; set; }
        public string facilitatorCode { get; set; }
    }
    public class IITokenBE
    {
        public string merchantBuyerId { get; set; }
        public string cardToken { get; set; }
        public string alias { get; set; }
    }
    public class IIAuthenticationBE
    {
        public string result { get; set; }
    }
}
