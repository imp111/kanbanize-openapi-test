using openapi_tests.Data;

namespace openapi_tests.Methods
{
    public class Workflows
    {
        public readonly RestClient _restClient;
        public RestRequest? _request;
        public RestResponse? _response;
        public RootWorkflows? _myDeserializedCard;

        public Workflows()
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });
        }

        public RootWorkflows GetAllBoards(int id) // returns a list of all cards
        {
            _request = new RestRequest("/boards/{board_id}/workflows")
                .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
                .AddUrlSegment("board_id", id);

            _response = _restClient.Get(_request);

            var listOfWorkflows = JsonConvert.DeserializeObject<RootWorkflows>(_response.Content);

            return listOfWorkflows;
        }
    }
}
