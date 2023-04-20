namespace openapi_tests
{
    public class WorkspaceTests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly RestClient _restClient;
        private readonly RestRequest _request;

        public WorkspaceTests(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;

            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            _request = new RestRequest("/workspaces").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
        }

        public int GetNumberOfWorkspaces()
        {
            // response
            var response = _restClient.Get(_request);
            
            // deserialize
            RootWorkspaces myDeserializedClass = JsonConvert.DeserializeObject<RootWorkspaces>(response.Content);
            
            return myDeserializedClass.data.Count;
        }

        [Fact]
        public void GetAllWorkspaces()
        {
            // response
            var response = _restClient.Get(_request);
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
            // request
            var request = new RestRequest("/workspaces/{workspace_id}")
                .AddUrlSegment("workspace_id", id);

            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // response
            var response = _restClient.Get(request);
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