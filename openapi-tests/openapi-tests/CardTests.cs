namespace openapi_tests
{
    public class CardTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public CardTests(ITestOutputHelper testOutputHelper)
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
            var request = new RestRequest("/cards");
            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // response
            var repsone = restClient.Get(request);
            string statusCode = repsone.StatusCode.ToString();

            // Assert
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine("A succesful response, Status code: 200 (OK)");
        }
    }
}
