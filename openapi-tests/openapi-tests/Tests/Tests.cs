using openapi_tests.Methods;

namespace openapi_tests.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly Cards CardsMethods;
        private List<RootCard> parameters;

        public Tests(ITestOutputHelper testOutputHelper, Cards cardsMethods)
        {
            _outputHelper = testOutputHelper;
            CardsMethods = cardsMethods;
        }

        [Fact]
        public void CreateACard()
        {
            var lastCard = CardsMethods.GetLastCard();
            var lastCardId = lastCard.card_id;

            var response = CardsMethods.CreateACardResponse();
            var myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(response.Content);
            var newCardId = myDeserializedCard.data.card_id;
            parameters.Add(myDeserializedCard);

            string statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
            Assert.NotEqual(lastCardId, newCardId);
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card with id {newCardId}");
        }

        [Fact]
        public void CheckIfCardIsCreated()
        {
            // Response
            var lastCard = CardsMethods.GetLastCard();
            var lastCardId = lastCard.card_id;
            int newCardId = parameters[parameters.Count - 1].data.card_id;

            Assert.Equal(newCardId, lastCardId);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {lastCardId} was successfuly created.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            var lastCard = CardsMethods.GetLastCard();
            int expectedPosition = CardsMethods.GetLastCardPosition(lastCard);

            int actualPosition = parameters[parameters.Count - 1].data.position;
            
            // Assert
            Assert.Equal(expectedPosition, actualPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {lastCard.card_id} is in position {expectedPosition}.");
        }

        [Fact]
        public void CheckIfCardIsCreatedWithExpectedParameters()
        {
            int id = CardsMethods.GetLastCardId();
            string expectedColor = CardsMethods.GetColorOfTheLastCard();
            int expectedPriority = CardsMethods.GetPriorityOfTheLastCard();

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
            int currentColumnId = GetColumnIdOfTheLastCard();
            int currentSection = GetSectionOfTheLastCard();

            var payload = new JObject
            {
                { "position", 0 },
                { "section", ++currentSection},
                { "column_id", ++currentColumnId},
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            _response = _restClient.Patch(_request.AddUrlSegment("card_id", id));

            // Assert
            Assert.Equal("OK", _response.StatusCode.ToString());
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} was moved to column: {currentSection} and section: {currentSection}.");
        }

        [Fact]
        public void CheckIfCardIsSuccessfulyMoved()
        {
            int id = GetLastCardId();
            
            _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
            _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);


            // Assert
            Assert.Equal("OK", _response.StatusCode.ToString());
            //_outputHelper.WriteLine($"Status code 200 - Card with id: {id} was moved to column: {currentSection} and section: {currentSection}.");
        }
    }
}