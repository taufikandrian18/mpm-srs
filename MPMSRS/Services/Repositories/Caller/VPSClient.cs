using System;
using System.Threading.Tasks;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces.ICaller;
using RestSharp;

namespace MPMSRS.Services.Repositories.Caller
{
    public class VPSClient : IVPSClient, IDisposable
    {
        readonly RestClient _client;

        public VPSClient(string config)
        {
            var options = new RestClientOptions(config);
            _client = new RestClient(options);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<UserMPMResponse> GetUserFromClient(string user, string token)
        {
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                RestClient restClient = new RestClient("https://vps.mpm-motor.com");
                var requestVps = new RestRequest("employee/api/1.0/employee/search-information", method: Method.Post);
                requestVps.AddHeader("Accept", "appliction/json");
                requestVps.AddParameter("employeeid", user, ParameterType.QueryString);
                requestVps.AddParameter("companyid", "MPM2", ParameterType.QueryString);
                requestVps.AddParameter("Content-Type", "application/json");
                requestVps.AddParameter("User", "altrovis", ParameterType.HttpHeader);
                requestVps.AddParameter("Authorization", token, ParameterType.HttpHeader);

                var responseVps = await restClient.ExecuteAsync<UserMPMResponse>(requestVps);

                return responseVps!.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TokenMPMData> GetTokenAuthorization()
        {
            try
            {
                var client = new RestClient("https://vps.mpm-motor.com/generatetoken");
                var request = new RestRequest("api/login", method: Method.Post);
                request.AddParameter("username", "altrovis", ParameterType.QueryString);
                request.AddParameter("password", "altrovis2019!", ParameterType.QueryString);
                var response = await client.ExecuteAsync<TokenMPMData>(request);
                return response!.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<userVpsSecretKey> GetVPSSecretKey(string token, LoginDealerDto model)
        {
            try
            {
                var client = new RestClient("https://vps.mpm-motor.com");
                var request = new RestRequest("portal/secretkey", method: Method.Post);

                string rawJson = "{ \r\n \"username\": \"" + model.Username+"\", \r\n  \"password\": \""+model.PasswordHash+"\", \r\n  \"password2\": \"-\", \r\n  \"dealer\": \""+model.DealerCode+"\"\r\n}";

                request.AddParameter("Content-Type", "application/json",ParameterType.HttpHeader);
                request.AddParameter("User", "Altrovis",ParameterType.HttpHeader);
                request.AddParameter("Authorization", token, ParameterType.HttpHeader);
                request.AddStringBody(rawJson, dataFormat: DataFormat.Json);
                var response = await client.ExecuteAsync<userVpsSecretKey>(request);
                return response!.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<userVpsSecretKey> GetVPSUserDealer(string token,string secretKey, LoginDealerDto model)
        {
            try
            {
                var client = new RestClient("https://vps.mpm-motor.com");
                var request = new RestRequest("portal/login", method: Method.Post);

                string rawJson = "{\r\n  \"kodedealer\": \""+model.DealerCode+"\", \r\n  \"username\": \""+model.Username+"\", \r\n  \"password\": \""+model.PasswordHash+"\", \r\n  \"password2\": \"-\",\r\n  \"skey\": \""+secretKey+"\",\r\n  \"startrow\": 0, \r\n  \"rowsperpage\": 100 \r\n}";

                request.AddParameter("Content-Type", "application/json", ParameterType.HttpHeader);
                request.AddParameter("User", "Altrovis", ParameterType.HttpHeader);
                request.AddParameter("Authorization", token, ParameterType.HttpHeader);
                request.AddStringBody(rawJson, dataFormat: DataFormat.Json);
                var response = await client.ExecuteAsync<userVpsSecretKey>(request);
                return response!.Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
