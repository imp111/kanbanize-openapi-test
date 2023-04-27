using openapi_tests.Methods;

namespace openapi_tests.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _outputHelper;
        private Cards CardsMethods;
        private Boards BoardsMethods;
        private Columns ColumnsMethods;
        private List<int> createdCardIds;

        public Tests(ITestOutputHelper testOutputHelper, Cards cards, Boards boards, Columns columns)
        {
            _outputHelper = testOutputHelper;
            createdCardIds = new List<int>();
            CardsMethods = cards;
            BoardsMethods = boards;
            ColumnsMethods = columns;
        }

        [Fact]
        public void CreateACard()
        {
            var lastCard = CardsMethods.GetLastCard();
            var lastCardId = lastCard.card_id;

            var response = CardsMethods.CreateACardResponse();
            var myDeserializedCard = JsonConvert.DeserializeObject<DataCard>(response.Content);
            var newCardId = myDeserializedCard.card_id;
            createdCardIds.Add(newCardId);

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

            int id = createdCardIds[createdCardIds.Count - 1];
            int newCardId = CardsMethods.GetCardById(id).card_id;

            Assert.Equal(newCardId, lastCardId);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {lastCardId} was successfuly created.");
        }

        [Fact]
        public void CheckIfCardIsInTheRightPosition()
        {
            var lastCard = CardsMethods.GetLastCard();
            int expectedPosition = CardsMethods.GetLastCardPosition(lastCard);

            int id = createdCardIds[createdCardIds.Count - 1];
            int actualPosition = CardsMethods.GetCardById(id).position;
            
            // Assert
            Assert.Equal(expectedPosition, actualPosition);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {lastCard.card_id} is in position {expectedPosition}.");
        }

        [Fact]
        public void CheckIfCardIsCreatedWithExpectedParameters()
        {
            var lastCard = CardsMethods.GetLastCard();
            string expectedColor = CardsMethods.GetLastCardColor(lastCard);
            int expectedPriority = CardsMethods.GetLastCardPriority(lastCard);

            int id = createdCardIds[createdCardIds.Count - 1];
            string actualColor = CardsMethods.GetCardById(id).color;
            int actualPriority = CardsMethods.GetCardById(id).priority;

            // Assert
            Assert.Equal($"{expectedColor}", actualColor);
            Assert.Equal(expectedPriority, actualPriority);
            _outputHelper.WriteLine($"Status code 200 - Card with id: {id} has color: {actualColor} and priority: {actualPriority}.");
        }

        [Fact]
        public void MoveCardToDifferentColumn()
        {
            int cardId = createdCardIds[createdCardIds.Count - 1];
            var card = CardsMethods.GetCardById(cardId);

            var board = BoardsMethods.GetBoardByName("Test Workspace");
            var boardId = board.board_id;

            var column = ColumnsMethods.GetColumnByName("In Progress", boardId); // we need the id of the board
            var columnSection = column.section;
            var columnId = column.column_id;
            var columnPosition = column.position;
            var columnName = column.name;
            
            var response = CardsMethods.MoveCardToDifferentColumn(cardId, columnSection, columnId, columnPosition);
            
            // Assert
            Assert.Equal("OK", response.StatusCode.ToString());
            _outputHelper.WriteLine($"Status code 200 - Card with id: {cardId} was moved to column: {columnName} and position: {columnPosition}.");
        }

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