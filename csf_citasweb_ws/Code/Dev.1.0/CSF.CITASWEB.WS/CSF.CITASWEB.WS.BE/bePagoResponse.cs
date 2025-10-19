using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class OrderPago
    {
        [DataMember]
        public string uniqueIdentifier { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public EntityPago entity { get; set; }
        [DataMember]
        public Country country { get; set; }
        [DataMember]
        public CurrencyPago currency { get; set; }
        [DataMember]
        public List<Products> products { get; set; }
        [DataMember]
        public CustomerPago customer { get; set; }
        [DataMember]
        public string paymentCode { get; set; }
        [DataMember]
        public string creation { get; set; }
        [DataMember]
        public string expiration { get; set; }
        [DataMember]
        public List<MetaData> metadata { get; set; }
        [DataMember]
        public State state { get; set; }
        [DataMember]
        public TargetType targetType { get; set; }
        [DataMember]
        public OrderType orderType { get; set; }
    }
    [DataContract]
    public class PaymentPago
    {
        [DataMember]
        public Card card { get; set; }
        [DataMember]
        public string uniqueIdentifier { get; set; }
        [DataMember]
        public Brand brand { get; set; }
        [DataMember]
        public BusinessService businessService { get; set; }
        [DataMember]
        public Operation operation { get; set; }
    }
    [DataContract]
    public class ResultPago
    {
        [DataMember]
        public string accepted { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public ProcessorResult processorResult { get; set; }
        [DataMember]
        public string processingTimestamp { get; set; }
    }
    [DataContract]
    public class ProcessorResult
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string transactionIdentifier { get; set; }
        [DataMember]
        public List<MetaDataPago> metadata { get; set; }
        [DataMember]
        public string originalResponse { get; set; }
    }
    [DataContract]
    public class MetaDataPago
    {
        [DataMember]
        public MetaData mdata1 { get; set; }
        [DataMember]
        public MetaData mdata2 { get; set; }
        [DataMember]
        public MetaData mdata3 { get; set; }
        [DataMember]
        public MetaData mdata4 { get; set; }
    }
    [DataContract]
    public class Card
    {
        [DataMember]
        public string bin { get; set; }
        [DataMember]
        public string lastPan { get; set; }
    }
    [DataContract]
    public class Brand
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class BusinessService
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class Operation
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class EntityPago
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string identifier { get; set; }
    }
    [DataContract]
    public class CurrencyPago
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string symbol { get; set; }
    }
    [DataContract]
    public class CustomerPago
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public Address address { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public Document document { get; set; }
    }
    [DataContract]
    public class bePagoResponse
    {
        [DataMember]
        public OrderPago order { get; set; }
        [DataMember]
        public PaymentPago payment { get; set; }
        [DataMember]
        public ResultPago result { get; set; }
        [DataMember]
        public string signature { get; set; }
        [DataMember]
        public string success { get; set; }
        [DataMember]
        public Message message { get; set; }
    }
    [DataContract]
    public class beOriginalResponse
    {
        [DataMember]
        public DataMap dataMap { get; set; }
        [DataMember]
        public OrderOriginalResponse order { get; set; }
    }
    [DataContract]
    public class DataMap
    {
        [DataMember]
        public string TRANSACTION_DATE { get; set; }
        [DataMember]
        public string ACTION_CODE { get; set; }
        [DataMember]
        public string CURRENCY { get; set; }
        [DataMember]
        public string TERMINAL { get; set; }
        [DataMember]
        public string TRACE_NUMBER { get; set; }
        [DataMember]
        public string BRAND { get; set; }
        [DataMember]
        public string CARD { get; set; }
        [DataMember]
        public string MERCHANT { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public string ADQUIRENTE { get; set; }
        [DataMember]
        public string ACTION_DESCRIPTION { get; set; }
        [DataMember]
        public string ID_UNICO { get; set; }
        [DataMember]
        public string AMOUNT { get; set; }
        [DataMember]
        public string PROCESS_CODE { get; set; }
        [DataMember]
        public string TRANSACTION_ID { get; set; }
        [DataMember]
        public string AUTHORIZATION_CODE { get; set; }
        [DataMember]
        public string ECI { get; set; }
        [DataMember]
        public string ECI_DESCRIPTION { get; set; }
        [DataMember]
        public string SIGNATURE { get; set; }
    }
    [DataContract]
    public class OrderOriginalResponse
    {
        [DataMember]
        public string purchaseNumber { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public string installment { get; set; }
        [DataMember]
        public string externalTransactionId { get; set; }
        [DataMember]
        public string authorizedAmount { get; set; }
        [DataMember]
        public string authorizationCode { get; set; }
        [DataMember]
        public string actionCode { get; set; }
        [DataMember]
        public string traceNumber { get; set; }
        [DataMember]
        public string transactionDate { get; set; }
        [DataMember]
        public string transactionId { get; set; }
    }
}
