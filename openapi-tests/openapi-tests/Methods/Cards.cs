using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openapi_tests.Methods
{
    public class Cards
    {
        public readonly RestClient _restClient;
        public RestRequest _request;
        public RestResponse _response;
        public RootCard _myDeserializedCard;

        public Cards()
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });

            _request = new RestRequest();
            _response = new RestResponse();
            _myDeserializedCard = new RootCard();
        }

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

        public string GetLastCardColor(DataCard lastCard)
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

        public RestResponse CreateACardResponse()
        {
            var lastCard = GetLastCard();

            int lastCardId = GetLastCardId(lastCard);
            int lastCardPosition = GetLastCardPosition(lastCard);

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
            _response = _restClient.Post(_request);

            return _response;
        }

        public RootCard CreateACardDeserializedResponse()
        {
            var lastCard = GetLastCard();

            int lastCardId = GetLastCardId(lastCard);
            int lastCardPosition = GetLastCardPosition(lastCard);

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
            _response = _restClient.Post(_request);
            _myDeserializedCard = JsonConvert.DeserializeObject<RootCard>(_response.Content);

            return _myDeserializedCard;
        }
    }
}
