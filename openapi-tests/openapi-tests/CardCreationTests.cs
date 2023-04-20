using Xunit.Abstractions;

namespace openapi_tests
{
    public class CardCreationTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public CardCreationTests(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;
        }

        [Fact]
        public void GetAllWorkspaces()
        {
            var restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            // request
            var request = new RestRequest("/workspaces");
            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // response
            var repsone = restClient.Get(request);
            string statusCode = repsone.StatusCode.ToString();

            // Assert
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine("A succesful response, Status code: 200 (OK)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetWorkspaceById(int id)
        {
            var restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            // request
            var request = new RestRequest("/workspaces/{workspace_id}")
                .AddUrlSegment("workspace_id", id);

            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // response
            var repsone = restClient.Get(request);
            string statusCode = repsone.StatusCode.ToString();

            // Assert
            if (id >= 1 && id <= 2)
            {
                Assert.Equal("OK", statusCode);
                _outputHelper.WriteLine("Status code - 200");
            }
            else
            {
                Assert.Equal("NotFound", statusCode);
                _outputHelper.WriteLine("Status code - 400");
            }
        }
    }
}