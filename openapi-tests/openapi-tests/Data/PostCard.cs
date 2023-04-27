using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openapi_tests.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Annotation
    {
        public string thread_id { get; set; }
        public string comment_id { get; set; }
        public int author_id { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
    }

    public class Attachment
    {
        public int id { get; set; }
        public string file_name { get; set; }
        public string link { get; set; }
        public int position { get; set; }
    }

    public class BlockReason
    {
        public int reason_id { get; set; }
        public int icon_type { get; set; }
        public int icon_id { get; set; }
        public string label { get; set; }
        public string color { get; set; }
        public int with_cards { get; set; }
        public int with_date { get; set; }
        public int with_users { get; set; }
        public int availability { get; set; }
        public int is_enabled { get; set; }
    }

    public class Checkpoint
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public string name { get; set; }
        public long value { get; set; }
    }

    public class ChildCardStats
    {
        public int child_card_size_sum { get; set; }
        public int finished_bottom_child_card_size_sum { get; set; }
        public int unfinished_bottom_child_card_size_sum { get; set; }
        public bool has_unfinished_child_cards { get; set; }
        public DateTime last_unfinished_child_card_deadline { get; set; }
    }

    public class Contributor
    {
        public int user_id { get; set; }
    }

    public class CustomField
    {
        public int field_id { get; set; }
        public object value { get; set; }
        public string display_value { get; set; }
        public List<Value> values { get; set; }
        public List<Contributor> contributors { get; set; }
        public List<File> files { get; set; }
        public List<Vote> votes { get; set; }
        public List<SelectedCard> selected_cards { get; set; }
    }

    public class DatumPostCard
    {
        public int card_id { get; set; }
        public string custom_id { get; set; }
        public int board_id { get; set; }
        public int workflow_id { get; set; }
        public string title { get; set; }
        public int owner_user_id { get; set; }
        public int type_id { get; set; }
        public string color { get; set; }
        public int section { get; set; }
        public int column_id { get; set; }
        public int lane_id { get; set; }
        public int position { get; set; }
        public string description { get; set; }
        public int size { get; set; }
        public int priority { get; set; }
        public DateTime deadline { get; set; }
        public Reporter reporter { get; set; }
        public DateTime created_at { get; set; }
        public int revision { get; set; }
        public DateTime last_modified { get; set; }
        public DateTime in_current_position_since { get; set; }
        public int is_blocked { get; set; }
        public BlockReason block_reason { get; set; }
        public ChildCardStats child_card_stats { get; set; }
        public int finished_subtask_count { get; set; }
        public int unfinished_subtask_count { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<CustomField> custom_fields { get; set; }
        public List<Sticker> stickers { get; set; }
        public List<int> tag_ids { get; set; }
        public List<int> co_owner_ids { get; set; }
        public List<int> watchers_ids { get; set; }
        public List<Annotation> annotations { get; set; }
        public List<Outcome> outcomes { get; set; }
        public List<Subtask> subtasks { get; set; }
        public List<LinkedCard> linked_cards { get; set; }
    }

    public class File
    {
        public int id { get; set; }
        public string file_name { get; set; }
        public string link { get; set; }
        public int position { get; set; }
    }

    public class LinkedCard
    {
        public int card_id { get; set; }
        public string link_type { get; set; }
    }

    public class Outcome
    {
        public int outcome_id { get; set; }
        public int field_id { get; set; }
        public int starting_value { get; set; }
        public int target_value { get; set; }
        public string @operator { get; set; }
        public object comment { get; set; }
        public int weight { get; set; }
        public DateTime created_at { get; set; }
        public List<Checkpoint> checkpoints { get; set; }
    }

    public class Reporter
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class RootPostCard
    {
        public List<DatumPostCard> data { get; set; }
    }

    public class SelectedCard
    {
        public int selected_card_id { get; set; }
        public int position { get; set; }
    }

    public class Sticker
    {
        public int id { get; set; }
        public int card_id { get; set; }
        public int sticker_id { get; set; }
    }

    public class Subtask
    {
        public int subtask_id { get; set; }
        public string description { get; set; }
        public int owner_user_id { get; set; }
        public DateTime finished_at { get; set; }
        public int position { get; set; }
        public List<Attachment> attachments { get; set; }
    }

    public class Value
    {
        public int value_id { get; set; }
        public int position { get; set; }
    }

    public class Vote
    {
        public int vote { get; set; }
        public string comment { get; set; }
        public int user_id { get; set; }
    }
}
