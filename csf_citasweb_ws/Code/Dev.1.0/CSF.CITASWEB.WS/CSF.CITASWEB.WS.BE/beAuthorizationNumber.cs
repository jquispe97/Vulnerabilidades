using System.Collections.Generic;

namespace CSF.CITASWEB.WS.BE
{
    public class beAuthorizationNumber
    {
        public string _type { get; set; }
        public beAnswer answer { get; set; }
        public string applicationProvider { get; set; }
        public string applicationVersion { get; set; }
        public string metadata { get; set; }
        public string mode { get; set; }
        public string serverDate { get; set; }
        public string serverUrl { get; set; }
        public string status { get; set; }
        public string ticket { get; set; }
        public string version { get; set; }
        public string webService { get; set; }
    }
    public class beAnswer 
    {
        public string _type { get; set; }
        public beCustomer customer { get; set; }
        public string orderCycle { get; set; }
        public beOrderDetails orderDetails { get; set; }
        public string orderStatus { get; set; }
        public string serverDate { get; set; }
        public string shopId { get; set; }
        public string subMerchantDetails { get; set; }
        public List<beTransactions> transactions { get; set; }

    }
    public class beCustomer
    {
        public string _type { get; set; }
        public beBillingDetails billingDetails { get; set; }
        public string email { get; set; }
        public beExtraDetails extraDetails { get; set; }
        public string reference { get; set; }
        public beShippingDetails shippingDetails { get; set; }
        public beShoppingCart shoppingCart { get; set; }
    }
    public class beBillingDetails
    {
        public string _type { get; set; }
        public string address { get; set; }
        public string category { get; set; }
        public string cellPhoneNumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string district { get; set; }
        public string firstName { get; set; }
        public string identityCode { get; set; }
        public string language { get; set; }
        public string lastName { get; set; }
        public string legalName { get; set; }
        public string phoneNumber { get; set; }
        public string state { get; set; }
        public string streetNumber { get; set; }
        public string title { get; set; }
        public string zipCode { get; set; }
    }
    public class beExtraDetails 
    {
        public string _type { get; set; }
        public string browserAccept { get; set; }
        public string browserUserAgent { get; set; }
        public string fingerPrintId { get; set; }
        public string ipAddress { get; set; }
    }
    public class beShippingDetails
    {
        public string _type { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string deliveryCompanyName { get; set; }
        public string district { get; set; }
        public string firstName { get; set; }
        public string identityCode { get; set; }
        public string lastName { get; set; }
        public string legalName { get; set; }
        public string phoneNumber { get; set; }
        public string shippingMethod { get; set; }
        public string shippingSpeed { get; set; }
        public string state { get; set; }
        public string streetNumber { get; set; }
        public string zipCode { get; set; }
    }
    public class beShoppingCart
    {
        public string _type { get; set; }
        public string cartItemInfo { get; set; }
        public string insuranceAmount { get; set; }
        public string shippingAmount { get; set; }
        public string taxAmount { get; set; }
    }
    public class beOrderDetails
    {
        public string _type { get; set; }
        public string metadata { get; set; }
        public string mode { get; set; }
        public string orderCurrency { get; set; }
        public int orderEffectiveAmount { get; set; }
        public string orderId { get; set; }
        public int orderTotalAmount { get; set; }
    }
    public class beTransactions
    {
        public string _type { get; set; }
        public int amount { get; set; }
        public string creationDate { get; set; }
        public string currency { get; set; }
        public string detailedErrorCode { get; set; }
        public string detailedErrorMessage { get; set; }
        public string detailedStatus { get; set; }
        public string effectiveStrongAuthentication { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public string metadata { get; set; }
        public string operationType { get; set; }
        public string paymentMethodToken { get; set; }
        public string paymentMethodType { get; set; }
        public string shopId { get; set; }
        public string status { get; set; }
        public beTransactionDetails transactionDetails { get; set; }
        public string uuid { get; set; }
    }
    public class beTransactionDetails 
    {
        public string _type { get; set; }
        public string acquirerNetwork { get; set; }
        public beCardDetails cardDetails { get; set; }
        public string creationContext { get; set; }
        public beDcc dcc { get; set; }
        public int effectiveAmount { get; set; }
        public string effectiveCurrency { get; set; }
        public string externalTransactionId { get; set; }
        public beFraudManagement fraudManagement { get; set; }
        public string liabilityShift { get; set; }
        public string mid { get; set; }
        public string nsu { get; set; }
        public string occurrenceType { get; set; }
        public string parentTransactionUuid { get; set; }
        public bePaymentMethodDetails paymentMethodDetails { get; set; }
        public string paymentMethodTokenPreviouslyRegistered { get; set; }
        public string preTaxAmount { get; set; }
        public int sequenceNumber { get; set; }
        public beSubscriptionDetails subscriptionDetails { get; set; }
        public string taxAmount { get; set; }
        public string taxRate { get; set; }
        public string taxRefundAmount { get; set; }
        public string tid { get; set; }
        public string userInfo { get; set; }
    }
    public class beCardDetails
    {
        public string _type { get; set; }
        public string authenticationResponse { get; set; }
        public beAuthorizationResponse authorizationResponse { get; set; }
        public beCaptureResponse captureResponse { get; set; }
        public string cardHolderName { get; set; }
        public string country { get; set; }
        public string effectiveBrand { get; set; }
        public string effectiveProductCode { get; set; }
        public string expectedCaptureDate { get; set; }
        public int expiryMonth { get; set; }
        public int expiryYear { get; set; }
        public string identityDocumentNumber { get; set; }
        public string identityDocumentType { get; set; }
        public string installmentCode { get; set; }
        public string installmentNumber { get; set; }
        public string issuerCode { get; set; }
        public string issuerName { get; set; }
        public string legacyTransDate { get; set; }
        public string legacyTransId { get; set; }
        public string manualValidation { get; set; }
        public beMarkAuthorizationResponse markAuthorizationResponse { get; set; }
        public string pan { get; set; }
        public string paymentMethodSource { get; set; }
        public string paymentSource { get; set; }
        public beThreeDSResponse threeDSResponse { get; set; }
    }
    public class beAuthorizationResponse
    {
        public string _type { get; set; }
        public int amount { get; set; }
        public string authorizationDate { get; set; }
        public string authorizationMode { get; set; }
        public string authorizationNumber { get; set; }
        public string authorizationResult { get; set; }
        public string currency { get; set; }
    }
    public class beCaptureResponse
    {
        public string _type { get; set; }
        public string captureDate { get; set; }
        public string captureFileNumber { get; set; }
        public string effectiveRefundAmount { get; set; }
        public string effectiveRefundCurrency { get; set; }
        public string refundAmount { get; set; }
        public string refundCurrency { get; set; }
    }
    public class beMarkAuthorizationResponse
    {
        public string _type { get; set; }
        public string amount { get; set; }
        public string authorizationDate { get; set; }
        public string authorizationNumber { get; set; }
        public string authorizationResult { get; set; }
        public string currency { get; set; }
    }
    public class beThreeDSResponse
    {
        public string _type { get; set; }
        public beAuthenticationResultData authenticationResultData { get; set; }
    }
    public class beAuthenticationResultData
    {
        public string _type { get; set; }
        public string brand { get; set; }
        public string cavv { get; set; }
        public string cavvAlgorithm { get; set; }
        public string eci { get; set; }
        public string enrolled { get; set; }
        public string signValid { get; set; }
        public string status { get; set; }
        public string transactionCondition { get; set; }
        public string xid { get; set; }
    }
    public class beDcc 
    {
        public string _type { get; set; }
    }
    public class beFraudManagement
    {
        public string _type { get; set; }
        public List<beRiskAnalysis> riskAnalysis { get; set; }
        public List<beRiskControl> riskControl { get; set; }
    }
    public class beRiskAnalysis
    {
        public string _type { get; set; }
        public List<beExtraInfo> extraInfo { get; set; }
        public string requestId { get; set; }
        public string resultCode { get; set; }
        public string score { get; set; }
        public string status { get; set; }
    }
    public class beExtraInfo
    {
        public string _type { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
    public class beRiskControl
    {
        public string _type { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
    public class bePaymentMethodDetails
    {
        public string _type { get; set; }
        public string authenticationResponse { get; set; }
        public beAuthorizationResponse authorizationResponse { get; set; }
        public beCaptureResponse captureResponse { get; set; }
        public string cardHolderName { get; set; }
        public string country { get; set; }
        public string effectiveBrand { get; set; }
        public string effectiveProductCode { get; set; }
        public string expectedCaptureDate { get; set; }
        public int expiryMonth { get; set; }
        public int expiryYear { get; set; }
        public string id { get; set; }
        public string identityDocumentNumber { get; set; }
        public string identityDocumentType { get; set; }
        public string installmentCode { get; set; }
        public string installmentNumber { get; set; }
        public string issuerCode { get; set; }
        public string issuerName { get; set; }
        public string legacyTransDate { get; set; }
        public string legacyTransId { get; set; }
        public string manualValidation { get; set; }
        public beMarkAuthorizationResponse markAuthorizationResponse { get; set; }
        public string paymentMethodSource { get; set; }
        public string paymentSource { get; set; }
    }
    public class beSubscriptionDetails
    {
        public string _type { get; set; }
        public string subscriptionId { get; set; }
    }
}
