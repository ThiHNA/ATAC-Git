using System.Net;
using APITest.Model;
using Automation.Core.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace APITest.Test
{
    [TestClass]
    public class UsersTest
    {
        [TestMethod]
        public void Verify_Get_List_Users_By_Page()
        {
            // Generate a random page number
            var randomPage = new Random().Next(1, 2 + 1);

            // Define the API endpoint with the random page number
            string endpoint = $"/api/users?page={randomPage}";

            // Make a GET request to the API using the endpoint and store the response
            RestResponse response = APIHelper.HandleMethodGet(endpoint);

            // Verify that the HTTP status code of the response is 200 OK
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verify that the page number is matched and contains at least one user
            var responseData = JsonConvert.DeserializeObject<ListUserModel>(response.Content);
            responseData.page.Should().Be(randomPage);
            responseData.data.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public void Verify_Create_User()
        {
            // Declare JsonPath
            string jsonPath = "Data/API_UserToCreate.json";

            // Read JSON data for creating a user from the JsonData class
            CreateUserRequestModel requestBody = JsonHelper.ReadJsonFile<CreateUserRequestModel>(jsonPath);

            // Make a POST request to the API to create a new user
            RestResponse response = APIHelper.HandleMethodPost("/api/users", requestBody);

            // Verify that the HTTP status code of the response is 201 Created
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Verify that the name and job in the response matches with the request body
            //var responseData = JsonHelper.ReadJsonFromString<CreateUserReponseModel>(response.Content);
            var responseData = JsonConvert.DeserializeObject<CreateUserReponseModel>(response.Content);
            responseData.name.Should().Be(requestBody.name);
            responseData.job.Should().Be(requestBody.job);

        }
    }
}
