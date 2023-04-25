using openapi_tests.Data;

namespace openapi_tests.Tests
{
    public class Card
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly RestClient _restClient;
        private readonly RestRequest _request;
        private readonly RestRequest _requests;
        private RestResponse _response;
        private RootCards? _myDeserializedCards = new RootCards();
        private RootCard? _myDeserializedCard = new RootCard();
        private RootWorkspaces? _myDeserializedWorkspaces = new RootWorkspaces();

        public Card(ITestOutputHelper testOutputHelper)
        {
            _outputHelper = testOutputHelper;
            _requests = new RestRequest("/cards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _request = new RestRequest("/cards/{card_id}").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = new RestResponse();  
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });
        }

        public RootCards GetListOfCards() // returns a list of all cards
        {
            _response = _restClient.Get(_requests);
            var listOfCards = JsonConvert.DeserializeObject<RootCards>(_response.Content);

            return listOfCards;
        }

        public DataCards GetLastCard() // returns the last created card
        {
            _response = _restClient.Get(_requests);
            var listOfCards = JsonConvert.DeserializeObject<RootCards>(_response.Content);
            int cardsCount = listOfCards.data.data.Count();
            int biggestId = 0;

            for (int i = 0; i < cardsCount; i++)
            {
                if (biggestId < listOfCards.data.data[i].card_id)
                {
                    biggestId = listOfCards.data.card_id;
                }
            }

            return listOfCards.data.data[biggestId];
        }

        public int GetLastCardId()
        {
            return GetLastCard().card_id;
        }

        public int GetPositionOfTheLastCard()
        {
            return GetLastCard().position;
        }

        public string GetColorOfTheLastCard()
        {
            return GetLastCard().color;
        }

        public int GetPriorityOfTheLastCard()
        {
            int id = GetLastCardId();
            _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
            _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);

            return _myDeserializedCard.data.priority;
        }

        [Fact]
        public void GetAllCards()
        {
            // response
            _response = _restClient.Get(_requests);
            string statusCode = _response.StatusCode.ToString();

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

            _requests.AddStringBody(payload.ToString(), DataFormat.Json);
            _response = _restClient.Post(_requests);

            string statusCode = _response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card with id {lastCardId}");
        }

        [Fact]
        public void CheckIfCardIsCreated()
        {
            int id = GetLastCardId();

            // Response
            _response = _restClient.Get(_request.AddUrlSegment("card_id", id));

            string statusCode = _response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} was successfuly created.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            int expectedPosition = GetPositionOfTheLastCard();
            string actualPosition = GetLastCard().position.ToString();
            int id = GetLastCardId();

            // Assert
            Assert.Equal($"{expectedPosition}", actualPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} is in position {actualPosition}.");
        }

        [Fact]
        public void CheckIfCardIsCreatedWithExpectedParameters()
        {
            int id = GetLastCardId();
            string expectedColor = GetColorOfTheLastCard();
            int expectedPriority = GetPriorityOfTheLastCard();

            // Response
            _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
            _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);
            var actualColor = _myDeserializedCard.data.color.ToString();
            var actualPriority = _myDeserializedCard.data.priority.ToString();

            // Assert
            Assert.Equal($"{expectedColor}", actualColor);
            Assert.Equal($"{expectedPriority}", actualPriority);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} has color: {actualColor} and priority: {actualPriority}.");
        }

        [Fact]
        public void MoveCardToDifferentColumn()
        {
            int id = GetLastCardId();
            int currentPosition = GetPositionOfTheLastCard();

            var payload = new JObject
            {
                { "position", 1 },
                { "section", 3},
                { "column_id", 13},
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            _response = _restClient.Patch(_request.AddUrlSegment("card_id", id));

            // Assert
            //_outputHelper.WriteLine($"Status code 200 - Card with id: {id} has color: {actualColor} and priority: {actualPriority}.");
        }
    }
}

//In Progress
//    "board_id": 2,
//    "workflow_id": 3,
//    "section": 3,
//    "column_id": 13,
//    "lane_id": 3,
//    "position": 1

//Requested
//    "board_id": 2,
//    "workflow_id": 3,
//    "section": 2,
//    "column_id": 12,
//    "position": 5,