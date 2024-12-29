using Automation.Core.Helpers;
using RestSharp;

namespace APITest.Test
{
    [TestClass]
    public class BaseTest
    {
        protected RestClient client;
        [TestInitialize]
        public void SetupRestClient()
        {
            client = new RestClient(ConfigurationHelper.GetValue<string>("url"));
        }
    }
}
