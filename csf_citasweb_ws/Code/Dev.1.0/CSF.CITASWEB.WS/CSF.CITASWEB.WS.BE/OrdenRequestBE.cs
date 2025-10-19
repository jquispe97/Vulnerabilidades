using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSF.CITASWEB.WS.BE
{
    [DataContract]
    public class OrdenRequestBE
    {
        public Order order { get; set; }
        public Settings settings { get; set; }
    }
    [DataContract]
    public class Order
    {
        public string number { get; set; }
        public Country country { get; set; }
        public Currency currency { get; set; }
        public string amount { get; set; }
        public Customer customer { get; set; }
        public List<Products> products { get; set; }
        public OrderType orderType { get; set; }
        public TargetType targetType { get; set; }
        public List<MetaData> metadata { get; set; }
    }
    [DataContract]
    public class OrderResponse
    {
        [DataMember]
        public string uniqueIdentifier { get; set; }
        [DataMember]
        public string number { get; set; }
        [DataMember]
        public Entity entity { get; set; }
        [DataMember]
        public Country country { get; set; }
        [DataMember]
        public Currency currency { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public List<Products> products { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string paymentCode { get; set; }
        [DataMember]
        public string creation { get; set; }
        [DataMember]
        public string expiration { get; set; }
        [DataMember]
        public State state { get; set; }
        [DataMember]
        public OrderType orderType { get; set; }
        [DataMember]
        public TargetType targetType { get; set; }
        [DataMember]
        public List<MetaData> metadata { get; set; }
    }
    [DataContract]
    public class Country
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class Entity
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string identifier { get; set; }
        [DataMember]
        public string logoUrl { get; set; }
    }
    [DataContract]
    public class State
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class Currency
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public Document document { get; set; }
        [DataMember]
        public Address address { get; set; }
    }
    [DataContract]
    public class Document
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string number { get; set; }
    }
    [DataContract]
    public class Address
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public List<String> levels { get; set; }
        [DataMember]
        public string line1 { get; set; }
        [DataMember]
        public string line2 { get; set; }
        [DataMember]
        public string zip { get; set; }
    }
    [DataContract]
    public class Products
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string quantity { get; set; }
        [DataMember]
        public string unitAmount { get; set; }
        [DataMember]
        public string amount { get; set; }
    }
    [DataContract]
    public class OrderType
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class TargetType
    {
        [DataMember]
        public string code { get; set; }
    }
    [DataContract]
    public class MetaData
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string value { get; set; }
    }
    [DataContract]
    public class Settings
    {
        [DataMember]
        public string language { get; set; }
        [DataMember]
        public AutoGenerate autogenerate { get; set; }
        [DataMember]
        public Expiration expiration { get; set; }
    }
    [DataContract]
    public class SettingsResponse
    {
        [DataMember]
        public string language { get; set; }
        [DataMember]
        public AutoGenerate autogenerate { get; set; }
        [DataMember]
        public List<String> brands { get; set; }
    }
    [DataContract]
    public class AutoGenerate
    {
        [DataMember]
        public string paymentCode { get; set; }
    }
    [DataContract]
    public class Expiration
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string date { get; set; }
    }
    [DataContract]
    public class Message
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string text { get; set; }
    }
    [DataContract]
    public class Data
    {
        [DataMember]
        public OrderResponse order { get; set; }

    }
    [DataContract]
    public class beOrdenResponse
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember]
        public Message message { get; set; }
        [DataMember]
        public Data data { get; set; }
        [DataMember]
        public SettingsResponse settings { get; set; }
    }
}
