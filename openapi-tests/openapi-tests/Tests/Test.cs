namespace openapi_tests.Tests
{
    public class Test
    {
        private readonly ITestOutputHelper _outputHelper;
        public readonly RestClient _restClient;
        public RestRequest? _request;
        public RestResponse? _response;
        public RootBoards _myDeserializedBoard;
        public RootCard _myDeserializedCard;
        public RootColumns _myDeserializedColumn;

        public Test(ITestOutputHelper testOutputHelper)
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            _outputHelper = testOutputHelper;
            _myDeserializedCard = new RootCard();
            _myDeserializedBoard = new RootBoards();
            _myDeserializedColumn = new RootColumns();
            _request = new RestRequest();
            _response = new RestResponse();
        }


        // Board Methods Section ----------------------------------------------------------------------------

        public RootBoards GetAllBoards() // returns a list of all cards
        {
            _request = new RestRequest("/boards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

            var listOfBoards = JsonConvert.DeserializeObject<RootBoards>(_response.Content);

            return listOfBoards;
        }

        public DatumBoards GetLastBoard() // returns the last created card
        {
            _request = new RestRequest("/boards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

            var listOfBoards = JsonConvert.DeserializeObject<RootBoards>(_response.Content);
            int boardsCount = listOfBoards.data.Count();
            int biggestId = 0;

            for (int i = 0; i < boardsCount; i++)
            {
                if (biggestId < listOfBoards.data[i].board_id)
                {
                    biggestId = listOfBoards.data[i].board_id;
                }
            }

            return listOfBoards.data[biggestId];
        }

        public DatumBoards GetBoardById(int id) // returns the last created card
        {
            _request = new RestRequest("/boards/{board_id}")
                .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
                .AddUrlSegment("card_id", id);

            _response = _restClient.Get(_request);

            var board = JsonConvert.DeserializeObject<DatumBoards>(_response.Content);

            return board;
        }

        public DatumBoards GetBoardByName(string name) // returns the last created card
        {
            _request = new RestRequest("/boards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

            var listOfBoards = JsonConvert.DeserializeObject<RootBoards>(_response.Content);
            int boardsCount = listOfBoards.data.Count;
            var board = new DatumBoards();

            for (int i = 0; i < boardsCount; i++)
            {
                if (name.ToLower() == listOfBoards.data[i].name)
                {
                    board = listOfBoards.data[i];
                }
            }

            return board;
        }

        // Cards Section ------------------------------------------------------------------------------------

        public RootCards GetAllCards() // returns a list of all cards
        {
            _request = new RestRequest("/cards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

            var listOfCards = JsonConvert.DeserializeObject<RootCards>(_response.Content);

            return listOfCards;
        }

        public DataCards GetLastCard() // returns the last created card
        {
            _request = new RestRequest("/cards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

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

        public DataCard GetCardById(int id) // returns the last created card
        {
            _request = new RestRequest("/cards/{card_id}")
                .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
                .AddUrlSegment("card_id", id);

            _response = _restClient.Get(_request);

            var card = JsonConvert.DeserializeObject<DataCard>(_response.Content);

            return card;
        }

        public int GetLastCardId(DataCards lastCard)
        {
            return lastCard.card_id;
        }

        public int GetLastCardPosition(DataCards lastCard)
        {
            return lastCard.position;
        }

        public string GetLastCardColor(DataCards lastCard)
        {
            return lastCard.color;
        }

        public int GetLastCardColumnId(DataCards lastCard)
        {
            return lastCard.column_id;
        }

        public int GetLastCardSection(DataCards lastCard)
        {
            return lastCard.section;
        }

        public int GetLastCardPriority(DataCards lastCard)
        {
            int id = lastCard.card_id;
            _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
            _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);

            return _myDeserializedCard.data.priority;
        }
        
        public RestResponse CreateACardResponse() // works
        {
            var lastCard = GetLastCard();

            int lastCardId = GetLastCardId(lastCard);
            int lastCardPosition = GetLastCardPosition(lastCard);

            int expectedNewCardId = lastCardId + 1;
            int expectedNewCardPosition = lastCardPosition + 1;

            var payload = new JObject
            {
                { "card_id", expectedNewCardId},
                { "board_id", 2 },
                { "workflow_id", 3 },
                { "title", $"Testing Post Method {expectedNewCardId}" },
                { "color", "2666be" },
                { "section", 2 },
                { "column_id", 12 },
                { "lane_id", 3 },
                { "position", expectedNewCardPosition },
                { "priority", 100 }
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            _response = _restClient.Post(_request);
            
            return _response;
        }

        public RootPostCard GetCreatedCardDeserializedResponse(RestResponse response)
        {
            var myDeserializedCard = JsonConvert.DeserializeObject<RootPostCard>(response.Content);
            
            return myDeserializedCard;
        }

        public bool IsCardCreatedSuccessfuly(int newCardId, int oldCardId)
        {
            if (newCardId > oldCardId)
            {
                return true;
            }

            return false;
        }

        public RestResponse MoveCardToDifferentColumn(int cardId, int section, int column_id, int position)
        {
            var payload = new JObject
            {
                { "position", position },
                { "section", section},
                { "column_id", ++column_id},
            };

            _request.AddStringBody(payload.ToString(), DataFormat.Json);
            _response = _restClient.Patch(_request.AddUrlSegment("card_id", cardId));

            return _response;
        }

        // Columns Section ----------------------------------------------------------------------------------

        public RootColumns GetAllColumns(int id) // returns a list of all cards
        {
            _request = new RestRequest("/boards/{board_id}/columns")
                .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
                .AddUrlSegment("board_id", id);

            _response = _restClient.Get(_request);

            var listOfColumns = JsonConvert.DeserializeObject<RootColumns>(_response.Content);

            return listOfColumns;
        }

        public DatumColumns GetColumnByName(string name, int id) // returns a list of all cards
        {
            _request = new RestRequest("/boards/{board_id}/columns")
                .AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd")
                .AddUrlSegment("board_id", id);

            _response = _restClient.Get(_request);

            var listOfColumns = JsonConvert.DeserializeObject<RootColumns>(_response.Content);
            var column = new DatumColumns();

            for (int i = 0; i < listOfColumns.data.Count; i++)
            {
                if (name == listOfColumns.data[i].name)
                {
                    column = listOfColumns.data[i];
                }
            }

            return column;
        }

        // Tests Section ------------------------------------------------------------------------------------

        [Fact]
        public void CreateACard()
        {
            var response = CreateACardResponse();
            
            Assert.Equal("OK", response.StatusCode.ToString());
            _outputHelper.WriteLine($"Status code 200 - Successfuly created card.");
        }

        [Fact]
        public void CheckIfCardIsCreated()
        {
            var lastCard = GetLastCard();
            var lastCardId = lastCard.card_id;

            var response = CreateACardResponse();
            var deserializedCard = GetCreatedCardDeserializedResponse(response);
            int newCardId = deserializedCard.data[0].card_id;

            bool check = IsCardCreatedSuccessfuly(newCardId, lastCardId);

            Assert.True(check);
            Assert.NotEqual(newCardId, lastCardId);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {newCardId} was successfuly created.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            var lastCard = GetLastCard();
            var lastCardPosition = lastCard.position;
            var lastCardId = lastCard.card_id;

            var response = CreateACardResponse();
            var deserializedCard = GetCreatedCardDeserializedResponse(response);
            int newCardPosition = deserializedCard.data[0].position;
            int newCardId = deserializedCard.data[0].card_id;

            // Assert
            Assert.NotEqual(newCardPosition, lastCardPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {newCardId} is in position {newCardPosition} and card with id: {lastCardId} is in position {lastCardPosition}.");
        }

        //[Fact, TestPriority(4)]
        //public void CheckIfCardIsCreatedWithExpectedParameters()
        //{
        //    var lastCard = GetLastCard();
        //    string expectedColor = GetLastCardColor(lastCard);
        //    int expectedPriority = GetLastCardPriority(lastCard);

        //    int id = 
        //    string actualColor = GetCardById(id).color;
        //    int actualPriority = GetCardById(id).priority;

        //    // Assert
        //    Assert.Equal($"{expectedColor}", actualColor);
        //    Assert.Equal(expectedPriority, actualPriority);
        //    _outputHelper.WriteLine($"Status code 200 - Card with id: {id} has color: {actualColor} and priority: {actualPriority}.");
        //}

        //[Fact, TestPriority(5)]
        //public void MoveCardToColumn()
        //{
        //    int cardId = 

        //    var board = GetBoardByName("Test Workspace");
        //    var boardId = board.board_id;

        //    var column = GetColumnByName("In Progress", boardId); // we need the id of the board
        //    var columnSection = column.section;
        //    var columnId = column.column_id;
        //    var columnPosition = column.position;
        //    var columnName = column.name;

        //    var response = MoveCardToDifferentColumn(cardId, columnSection, columnId, columnPosition);

        //    // Assert
        //    Assert.Equal("OK", response.StatusCode.ToString());
        //    _outputHelper.WriteLine($"Status code 200 - Card with id: {cardId} was moved to column: {columnName} and position: {columnPosition}.");
        //}

        //[Fact]
        //public void CheckIfCardIsSuccessfulyMoved()
        //{
        //    int id = GetLastCardId();

        //    _response = _restClient.Get(_request.AddUrlSegment("card_id", id));
        //    _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);

        //    var a = BoardsMethods.GetBoardByName();

        //    // Assert
        //    Assert.Equal("OK", _response.StatusCode.ToString());
        //    //_outputHelper.WriteLine($"Status code 200 - Card with id: {id} was moved to column: {currentSection} and section: {currentSection}.");
        //}
    }
}
