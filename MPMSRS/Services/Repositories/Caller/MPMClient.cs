using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces.ICaller;
using RestSharp;

namespace MPMSRS.Services.Repositories.Caller
{
    public class MPMClient : IMPMClient, IDisposable
    {
        readonly RestClient _client;

        public MPMClient(string config)
        {
            var options = new RestClientOptions(config);
            _client = new RestClient(options);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public static XmlDocument RemoveXmlns(string xml)
        {
            XDocument d = XDocument.Parse(xml);
            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(d.CreateReader());

            return xmlDocument;
        }

        public async Task<string> GetLoginAuthorization(LoginDto model)
        {
            try
            {
                //var s = getMpmResponse(model);

                RestClient restClient = new RestClient("https://hr.mpm-motor.com:8080/MPM-TRN");

                string schemas = "'http://schemas.xmlsoap.org/soap/envelope/'";
                string isUserVld = "'http://srvc.hrbase.sps.com'";


                string rawXml = "<Envelope xmlns=" + schemas + "><Body><isUserValid xmlns=" + isUserVld + "><userId>" + model.UserId.Trim() + "</userId><password>" + model.Password.Trim() + "</password></isUserValid></Body></Envelope>";

                XmlDocument xDoc = new XmlDocument();
                XmlElement Envelope = xDoc.CreateElement(string.Empty, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                xDoc.AppendChild(Envelope);
                XmlElement Body = xDoc.CreateElement("Body");
                Envelope.AppendChild(Body);
                XmlElement isUserValid = xDoc.CreateElement(string.Empty, "isUserValid", "http://srvc.hrbase.sps.com");
                Body.AppendChild(isUserValid);
                XmlElement userId = xDoc.CreateElement(string.Empty, "userId", string.Empty);
                XmlText text1 = xDoc.CreateTextNode(model.UserId);
                userId.AppendChild(text1);
                isUserValid.AppendChild(userId);

                XmlElement password = xDoc.CreateElement(string.Empty, "password", string.Empty);
                XmlText text2 = xDoc.CreateTextNode(model.Password);
                password.AppendChild(text2);
                isUserValid.AppendChild(password);

                var request = new RestRequest("services/UserValidationSrvcs", Method.Post);
                //var t = RemoveXmlns(xDoc.InnerXml);
                request.AddStringBody(rawXml, dataFormat: DataFormat.Xml);
                request.AddParameter("wsdl", "", ParameterType.QueryString);
                request.AddHeader("Content-Type", "application/xml");
                request.AddHeader("SOAPAction", "");
                //request.AddParameter("Content-Type", "application/x-www-form-urlencoded",ParameterType.HttpHeader);
                //request.AddParameter("SOAPAction", "",ParameterType.HttpHeader);
                RestResponse result = new RestResponse();
                result = await restClient.ExecuteAsync(request);
                Console.WriteLine(result);

                return result.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResetPasswordSerialization> MPMResetPassword(string paramUserId, string paramEmail)
        {
            try
            {
                RestClient restClient = new RestClient("https://hr.mpm-motor.com:8080/MPM-TRN/");

                string schemas = "'http://schemas.xmlsoap.org/soap/envelope/'";
                string isUserVld = "'http://srvc.hrbase.sps.com'";

                string rawXml = "<Envelope xmlns=" + schemas + "><Body><resetPassword xmlns=" + isUserVld + "><userId>" + paramUserId.Trim() + "</userId><email>" + paramEmail.Trim() + "</email></resetPassword></Body></Envelope>";
                var request = new RestRequest("services/ResetPasswordSrvc", Method.Post);
                request.AddParameter("wsdl", "", ParameterType.QueryString);
                request.AddStringBody(rawXml, dataFormat: DataFormat.Xml);
                request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
                request.AddParameter("SOAPAction", "", ParameterType.HttpHeader);

                var response = await restClient.ExecuteAsync<ResetPasswordSerialization>(request);

                return response!.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginSerialization> GetMPMOrange(LoginDto model)
        {
            try
            {
                RestClient restClient = new RestClient("https://hr.mpm-motor.com:8080/MPM-TRN/");

                string schemas = "'http://schemas.xmlsoap.org/soap/envelope/'";
                string isUserVld = "'http://srvc.hrbase.sps.com'";

                string rawXml = "<Envelope xmlns=" + schemas + "><Body><isUserValid xmlns=" + isUserVld + "><userId>" + model.UserId.Trim() + "</userId><password>" + model.Password.Trim() + "</password></isUserValid></Body></Envelope>";
                var request = new RestRequest("services/UserValidationSrvcs",Method.Post);
                request.AddParameter("wsdl", "", ParameterType.QueryString);
                request.AddStringBody(rawXml, dataFormat: DataFormat.Xml);
                request.AddParameter("Content-Type", "application/xml",ParameterType.HttpHeader);
                request.AddParameter("SOAPAction", "",ParameterType.HttpHeader);

                var response = await restClient.ExecuteAsync<LoginSerialization>(request);

                return response!.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
