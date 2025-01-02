using RestSharp;

namespace Automation.Core.Helpers
{
    public class APIHelper
    {
        protected static RestClient client = new RestClient(ConfigurationHelper.GetValue<string>("url"));

        public static RestResponse HandleMethodGet(string endpoint) 
        {
            var request = new RestRequest(endpoint, Method.Get);
            RestResponse response = client.Execute(request);
            return response;
        }

        public static RestResponse HandleMethodPost(string endpoint, object requestBody)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(requestBody); 

            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
