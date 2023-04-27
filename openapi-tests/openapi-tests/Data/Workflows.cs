namespace openapi_tests.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DatumWorkflows
    {
        public int type { get; set; }
        public int position { get; set; }
        public int is_enabled { get; set; }
        public int is_collapsible { get; set; }
        public string? name { get; set; }
        public int workflow_id { get; set; }
    }

    public class RootWorkflows
    {
        public List<DatumWorkflows>? data { get; set; }
    }
}