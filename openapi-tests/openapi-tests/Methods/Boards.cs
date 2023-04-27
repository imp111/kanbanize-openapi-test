using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openapi_tests.Methods
{
    public class Boards
    {
        public readonly RestClient _restClient;
        public RestRequest? _request;
        public RestResponse? _response;
        public RootBoards? _myDeserializedCard;

        public Boards()
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });
        }

        public RootBoards GetAllBoards() // returns a list of all cards
        {
            _request = new RestRequest("/boards").AddHeader("apikey", "3ZIPG0qqf7fBuUQ8uqCt7N7iTKoGuOhHSwRRwdtd");
            _response = _restClient.Get(_request);

            if (_response.Content != null)
            {
                return JsonConvert.DeserializeObject<RootBoards>(_response.Content);
            }

            return new RootBoards();
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

            if (_response.Content == null)
            {
                throw new Exception();
            }

            var listOfBoards = JsonConvert.DeserializeObject<RootBoards>(_response.Content);

            if (listOfBoards == null)
            {
                throw new Exception();
            }

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
    }
}
