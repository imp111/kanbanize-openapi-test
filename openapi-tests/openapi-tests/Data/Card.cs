namespace openapi_tests.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ChildCardStatsCards
    {
        public int child_card_size_sum { get; set; }
        public int finished_bottom_child_card_size_sum { get; set; }
        public int unfinished_bottom_child_card_size_sum { get; set; }
        public bool has_unfinished_child_cards { get; set; }
        public object? last_unfinished_child_card_deadline { get; set; }
    }

    public class DataCard
    {
        public int card_id { get; set; }
        public object? custom_id { get; set; }
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
        public string? description { get; set; }
        public object? size { get; set; }
        public int priority { get; set; }
        public object? deadline { get; set; }
        public Reporter? reporter { get; set; }
        public DateTime created_at { get; set; }
        public int revision { get; set; }
        public DateTime last_modified { get; set; }
        public DateTime in_current_position_since { get; set; }
        public int is_blocked { get; set; }
        public object? block_reason { get; set; }
        public ChildCardStats? child_card_stats { get; set; }
        public int finished_subtask_count { get; set; }
        public int unfinished_subtask_count { get; set; }
        public List<object>? attachments { get; set; }
        public List<object>? custom_fields { get; set; }
        public List<object>? stickers { get; set; }
        public List<object>? tag_ids { get; set; }
        public List<object>? co_owner_ids { get; set; }
        public List<object>? watcher_ids { get; set; }
        public List<object>? annotations { get; set; }
        public List<object>? outcomes { get; set; }
        public List<object>? subtasks { get; set; }
        public List<object>? linked_cards { get; set; }
    }

    public class ReporterCards
    {
        public string? type { get; set; }
        public int value { get; set; }
    }

    public class RootCard
    {
        public DataCard? data { get; set; }
    }
}