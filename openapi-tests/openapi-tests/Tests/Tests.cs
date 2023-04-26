﻿using openapi_tests.Methods;

namespace openapi_tests.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly Cards CardsMethods;

        public Tests(ITestOutputHelper testOutputHelper, Cards cardsMethods)
        {
            _outputHelper = testOutputHelper;
            CardsMethods = cardsMethods;
        }

        [Fact]
        public void CreateACard()
        {
            var response = CardsMethods.CreateACardResponse();
            var lastCard = CardsMethods.GetLastCard();
            var lastCardId = lastCard.card_id;

            string statusCode = response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card with id {lastCardId}");
        }

        [Fact]
        public void CheckIfCardIsCreated()
        {
            int id = cardsMethods.GetLastCardId();

            // Response
            cardsMethods._response = _restClient.Get(_request.AddUrlSegment("card_id", id));

            string statusCode = _response.StatusCode.ToString();
            Assert.Equal("OK", statusCode);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} was successfuly created.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            int expectedPosition = CardsMethods.GetPositionOfTheLastCard();
            string actualPosition = CardsMethods.GetLastCard().position.ToString();
            int id = CardsMethods.GetLastCardId();

            // Assert
            Assert.Equal($"{expectedPosition}", actualPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} is in position {actualPosition}.");
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


//public RootCards GetAllCards() // returns a list of all cards
//{
//    _request = new RestRequest("/cards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
//    _response = _restClient.Get(_request);

//    var listOfCards = JsonConvert.DeserializeObject<RootCards>(_response.Content);

//    return listOfCards;
//}

//public DataCard GetCardById(int id) // returns the last created card
//{
//    _request = new RestRequest("/cards/{card_id}")
//        .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
//        .AddUrlSegment("card_id", id);

//    _response = _restClient.Get(_request);

//    var card = JsonConvert.DeserializeObject<DataCard>(_response.Content);

//    return card;
//}

//public DataCards GetLastCard(RootCards listOfCards) // returns the last created card
//{
//    listOfCards = JsonConvert.DeserializeObject<RootCards>(_response.Content);
//    int cardsCount = listOfCards.data.data.Count();
//    int biggestId = 0;

//    for (int i = 0; i < cardsCount; i++)
//    {
//        if (biggestId < listOfCards.data.data[i].card_id)
//        {
//            biggestId = listOfCards.data.card_id;
//        }
//    }

//    return listOfCards.data.data[biggestId];
//}

//public int GetLastCardId(DataCards lastCard)
//{
//    return lastCard.card_id;
//}

//public int GetPositionOfTheLastCard(DataCards lastCard)
//{
//    return lastCard.position;
//}

//public string GetColorOfTheLastCard(DataCard lastCard)
//{
//    return lastCard.color;
//}

//public int GetColumnIdOfTheLastCard(DataCards lastCard)
//{
//    return lastCard.column_id;
//}

//public int GetSectionOfTheLastCard(DataCards lastCard)
//{
//    return lastCard.section;
//}

//public int GetPriorityOfTheLastCard(DataCards lastCard)
//{
//    int id = lastCard.card_id;
//    _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
//    _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);

//    return _myDeserializedCard.data.priority;
//}
