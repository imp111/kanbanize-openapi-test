namespace openapi_tests.Data
{
    public class DatumWorkspaces
    {
        public int workspace_id { get; set; }
        public int type { get; set; }
        public int is_archived { get; set; }
        public string? name { get; set; }
    }

    public class RootWorkspaces
    {
        public List<DatumWorkspaces>? data { get; set; }
    }
}