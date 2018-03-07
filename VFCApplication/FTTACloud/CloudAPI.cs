using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFCApplication.FTTACloud
{
    class CloudAPI
    {
        private CloudConnection cloud;
        public User CurrentUser { get; private set; }

        public CloudAPI(string credentials)
        {
            cloud = new CloudConnection(credentials);
        }
        
        public void Login(string email, string password)
        {
            var request = new RestRequest();
            request.Resource = "Users.svc/Login";
            request.AddParameter("email", email, ParameterType.QueryString);
            request.AddParameter("password", password, ParameterType.QueryString);

            CurrentUser = cloud.Get<User>(request);
        }

        public string GetStatus()
        {
            var request = new RestRequest();
            request.Resource = "Lookups.svc/Status";

            var response = cloud.Get<Dictionary<string, string>>(request);
            return response["version"];
        }

        public List<Device> GetDeviceList()
        {
            var request = new RestRequest();
            request.Resource = "Devices.svc/AllDevicesSummary";
            request.AddParameter("userGUID", CurrentUser.GUID, ParameterType.QueryString);
            request.AddParameter("includeArchived", "false", ParameterType.QueryString);

            var response = cloud.Get<List<Device>>(request);
            return response;
        }
 
        /// <summary>
        /// Respresents a connection to Lascar's FilesThruTheAir cloud
        /// TODO add asychronous variations of calls
        /// </summary>
        private class CloudConnection
        {
            const string BaseURL = "https://api.wifisensorcloud.com";

            readonly string _APIToken;
            RestClient client;

            /// <summary>
            /// Construct the client object
            /// </summary>
            /// <param name="APIToken">FilesThruTheAir API Token</param>
            public CloudConnection(string APIToken)
            {
                _APIToken = APIToken;
                client = new RestClient(BaseURL);
            }

            /// <summary>
            /// Send object-based GET request to server
            /// </summary>
            /// <typeparam name="T">Generic data-holding object</typeparam>
            /// <param name="request">Request representation</param>
            /// <returns>Object T, containing data from the GET</returns>
            public T Get<T>(RestRequest request) where T : new()
            {
                request.Method = Method.GET;
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("APIToken", _APIToken, ParameterType.QueryString);

                // Retrieve response using the RestSharp client
                var response = client.Execute<T>(request);
                if (response.ErrorException != null)
                    throw new CloudException(response.Content);
                return response.Data;
            }

            /// <summary>
            /// Send raw JSON-based GET request to server
            /// </summary>
            /// <param name="request">Request representation</param>
            /// <returns>Dictionary object holding the JSON data</returns>
            /*public Dictionary<string, string> RawGet(RestRequest request)
            {
                request.Method = Method.GET;
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("APIToken", _APIToken, ParameterType.RequestBody);

                var response = client.Execute(request);

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            }*/

            /// <summary>
            /// Send object-based POST request to server
            /// </summary>
            /// <typeparam name="T">Generic data-holding object</typeparam>
            /// <param name="request">Request representation</param>
            /// <returns>Object T, containing data from the POST</returns>
            public T Post<T>(RestRequest request) where T : class, new()
            {
                request.Method = Method.POST;
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("APIToken", _APIToken, ParameterType.RequestBody);

                var response = client.Execute<T>(request);
                if (response.ErrorException != null)
                    throw new CloudException(response.Content);
                return response.Data;
            }

        }

        public class CloudException : Exception
        {
            public CloudException(string message) : base(message) { }
        }
    }

    
}
