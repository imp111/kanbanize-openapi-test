namespace openapi_tests
{
    public class CardCreationTests
    {
        [Fact]
        public void Test1()
        {
            var restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            // request
            var request = new RestRequest("/workspaces/{workspace_id}")
                .AddUrlSegment("workspace_id", 2);

            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

           // response
           var repsone = restClient.Get(request);
        }
    }
}