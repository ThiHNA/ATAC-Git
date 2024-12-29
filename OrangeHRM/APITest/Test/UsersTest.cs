using System.Net;
using APITest.Model;
using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;

namespace APITest.Test
{
    [TestClass]
    public class UsersTest : BaseTest
    {
        [TestMethod]
        public void Verify_Get_List_Users_By_Page()
        {
            var randomPage = new Random().Next(1, 2 + 1);
            var request = new RestRequest($"/api/users?page={randomPage}", Method.Get);
            RestResponse response = client.Execute(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseData = JsonConvert.DeserializeObject<ListUserModel>(response.Content);
            responseData.page.Should().Be(randomPage);
            responseData.data.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public void Verify_Create_User()
        {
            var requestBody = new CreateUserRequestModel();
            requestBody.name = "Thi";
            requestBody.job = "Member";

            var request = new RestRequest("/api/users", Method.Post);
            request.AddJsonBody(requestBody);

            RestResponse response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseData = JsonConvert.DeserializeObject<CreateUserReponseModel>(response.Content);
            responseData.name.Should().Be(requestBody.name);
            responseData.job.Should().Be(requestBody.job);

        }
    }
}
