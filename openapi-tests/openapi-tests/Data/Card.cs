namespace openapi_tests.Data
{
    public record Card
    (
        int WorkspaceId,
        int Type,
        int IsArchived,
        string Name
    );
}

//response.Content
// "{\"data\":[
//      {\"workspace_id\":1,\"type\":1,\"is_archived\":0,\"name\":\"Demo Workspace\"},
//      {\"workspace_id\":2,\"type\":1,\"is_archived\":0,\"name\":\"Test Workspace 2\"}
//  ]}"