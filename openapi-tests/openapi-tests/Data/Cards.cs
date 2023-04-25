// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
namespace openapi_tests.Data
{
    public class DataCards
    {
        public Pagination? pagination { get; set; }
        public List<DataCards>? data { get; set; }
        public int card_id { get; set; }
        public string? custom_id { get; set; }
        public int board_id { get; set; }
        public int workflow_id { get; set; }
        public string? title { get; set; }
        public object? owner_user_id { get; set; }
        public object? type_id { get; set; }
        public string? color { get; set; }
        public int section { get; set; }
        public int column_id { get; set; }
        public int lane_id { get; set; }
        public int position { get; set; }
    }

    public class Pagination
    {
        public int all_pages { get; set; }
        public int current_page { get; set; }
        public int results_per_page { get; set; }
    }

    public class RootCards
    {
        public DataCards? data { get; set; }
    }
}
