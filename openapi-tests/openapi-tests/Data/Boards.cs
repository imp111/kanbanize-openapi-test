namespace openapi_tests.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumBoards
    {
        public int board_id { get; set; }
        public int workspace_id { get; set; }
        public int is_archived { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }

    public class RootBoards
    {
        public List<DatumBoards>? data { get; set; }
    }
}
