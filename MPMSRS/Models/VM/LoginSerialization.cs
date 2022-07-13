using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MPMSRS.Models.VM
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class LoginSerialization
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public ResponseBody Body { get; set; }
    }
    public class ResponseBody
    {
        [XmlElement(ElementName = "isUserValidResponse", Namespace = "http://srvc.hrbase.sps.com")]
        public isUserValidResponse isUserValidResponse { get; set; }
    }
    public class isUserValidResponse
    {
        [XmlElement(ElementName = "isUserValidReturn")]
        public string isUserValidReturn { get; set; }
    }

    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class ResetPasswordSerialization
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public ResponseBodyReset Body { get; set; }
    }

    public class ResponseBodyReset
    {
        [XmlElement(ElementName = "resetPasswordResponse", Namespace = "http://srvc.hrbase.sps.com")]
        public resetPasswordResponse resetPasswordResponse { get; set; }
    }

    public class resetPasswordResponse
    {
        [XmlElement(ElementName = "resetPasswordReturn")]
        public string resetPasswordReturn { get; set; }
    }

    public class TokenMPMData
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string key { get; set; }
        public long expires_in { get; set; }
    }

    public class UserMPMResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public UserMPMData[] data { get; set; }
        public UserMPMDataPage page { get; set; }
    }

    public class UserMPMData
    {
        public string employeeid { get; set; }
        public string companyid { get; set; }
        public string worklocation { get; set; }
        public string display_name { get; set; }
        public string jobtitle { get; set; }
        public string picture { get; set; }
        public string division { get; set; }
        public string departement { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class UserMPMDataPage
    {
        public string first_page { get; set; }
        public string next_page { get; set; }
        public string back_page { get; set; }
    }

    public static class Constants
    {
        public const string Issuer = Audiance;
        public const string Audiance = "https://localhost:5001";
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";
    }

    public class UserMPMLogin
    {
        public UserDto Users { get; set; }
        public AuthenticatedUserResponse Token { get; set; }
    }

    public class AuthenticatedUserResponse
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }
    }

    public class userVpsSecretKey
    {
        public bool status { get; set; }
        public List<string> data { get; set; }
    }
}
