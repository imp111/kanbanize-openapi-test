namespace openapi_tests.Tests
{
    public class Card
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly RestClient _restClient;
        private readonly RestRequest _request;

        public Card(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;
            _request = new RestRequest("/cards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

        }

        public int GetLastCardId()
        {
            var response = _restClient.Get(_request);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response.Content);

            var biggestId = 0;

            for (int i = 0; i < myDeserializedClass.data.data.Count; i++)
            {
                if (biggestId < myDeserializedClass.data.data[i].card_id)
                {
                    biggestId = myDeserializedClass.data.data[i].card_id;
                }
            }

            return biggestId;
        }

        [Fact]
        public void GetAllCards()
        {
            // response
            var response = _restClient.Get(_request);
            string statusCode = response.StatusCode.ToString();

            // Assert
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine("A succesful response, Status code: 200 (OK)");
        }

        [Fact]
        public void CreateACard()
        {
            int lastCardId = GetLastCardId();

            var payload = new JObject
            {
                { "card_id", ++lastCardId },
                { "board_id", 2 },
                { "workflow_id", 3 },
                { "title", $"Testing Post Method {lastCardId}" },
                { "color", "2666be" },
                { "section", 2 },
                { "column_id", 12 },
                { "lane_id", 3 },
                { "position", 2 },
                { "priority", 250 }
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            var respone = _restClient.Post(_request);

            string statusCode = respone.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card with id {lastCardId}");
        }
    }
}

