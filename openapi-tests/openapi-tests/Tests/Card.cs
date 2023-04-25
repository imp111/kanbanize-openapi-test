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
            var myDeserializedClass = JsonConvert.DeserializeObject<RootCards>(response.Content);

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

        public int GetPositionOfTheLastCard()
        {
            var response = _restClient.Get(_request);
            var myDeserializedClass = JsonConvert.DeserializeObject<RootCards>(response.Content);

            var lastCardPosition = 0;

            for (int i = 0; i < myDeserializedClass.data.data.Count; i++)
            {
                if (lastCardPosition < myDeserializedClass.data.data[i].position)
                {
                    lastCardPosition = myDeserializedClass.data.data[i].position;
                }
            }

            return lastCardPosition;
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
            int lastCardPosition = GetPositionOfTheLastCard();

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
                { "position", ++lastCardPosition },
                { "priority", 100 }
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            var respone = _restClient.Post(_request);

            string statusCode = respone.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card with id {lastCardId}");
        }

        [Fact]
        public void CheckIfCardIsCreated()
        {
            int id = GetLastCardId();

            // Request
            var request = new RestRequest("/cards/{card_id}")
                .AddUrlSegment("card_id", id);

            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // Response
            var response = _restClient.Get(request);

            string statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} is created successfuly.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            int id = GetLastCardId();
            int expectedPosition = GetPositionOfTheLastCard();

            // Request
            var request = new RestRequest("/cards/{card_id}")
                .AddUrlSegment("card_id", id);

            request.AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");

            // Response
            var response = _restClient.Get(request);
            var myDeserializedClass = JsonConvert.DeserializeObject<RootCard>(response.Content);
            var actualPosition = myDeserializedClass.data.position.ToString();

            // Assert
            Assert.Equal($"{expectedPosition}", actualPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} is in position {actualPosition}.");
        }

        [Fact]
        public void CheckIfCardIsCreatedWithExpectedParameters()
        {
        }
    }
}

