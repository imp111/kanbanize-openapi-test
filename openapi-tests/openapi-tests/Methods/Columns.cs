using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openapi_tests.Methods
{
    public class Columns
    {
        public readonly RestClient _restClient;
        public RestRequest? _request;
        public RestResponse? _response;
        public RootColumns? _myDeserializedCard;

        public Columns()
        {
            _restClient = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("https://none7t.kanbanize.com/api/v2"),
                Authenticator = new HttpBasicAuthenticator("alexsandartenev@gmail.com", "praseta123")
            });
        }

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
    }
}
