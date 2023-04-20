namespace openapi_tests
{
    public class WorkspaceTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public WorkspaceTests(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;
        }

        public int GetNumberOfWorkspaces()
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
            var response = restClient.Get(request);
            
            // deserialize
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);
            int length = myDeserializedClass.data.Count;

            return length;
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
            var response = restClient.Get(request);
            string statusCode = response.StatusCode.ToString();

            // Assert
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine("A succesful response, Status code: 200 (OK)");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
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
            var response = restClient.Get(request);
            string statusCode = response.StatusCode.ToString();
            int workspacesCount = GetNumberOfWorkspaces();
            // Assert
            if (id >= 0 && id <= workspacesCount)
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