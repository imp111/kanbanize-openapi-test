namespace openapi_tests.Data
{
    public class Datum
    {
        public int workspace_id { get; set; }
        public int type { get; set; }
        public int is_archived { get; set; }
        public string? name { get; set; }
    }

    public class Root
    {
        public List<Datum>? data { get; set; }
    }
}