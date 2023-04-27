namespace openapi_tests.Data
{
    public class Columns
    {
        // RootColumns myDeserializedClass = JsonConvert.DeserializeObject<RootColumns>(myJsonResponse);
        public class DatumColumns
        {
            public int column_id { get; set; }
            public int workflow_id { get; set; }
            public int section { get; set; }
            public object? parent_column_id { get; set; }
            public int position { get; set; }
            public string? name { get; set; }
            public string? description { get; set; }
            public string? color { get; set; }
            public int limit { get; set; }
            public int cards_per_row { get; set; }
            public int flow_type { get; set; }
            public object? card_ordering { get; set; }
        }

        public class RootColumns
        {
            public List<DatumColumns>? data { get; set; }
        }
    }
}
