using MongoDB.Bson;
using System.Text.Json.Nodes;

namespace FeaturesForMongoDB.Abstrations
{
    public interface IContext
    {
        void CreateBsonDocumentList(List<BsonDocument> documents);
        void UpdateBsonDocumentList(List<BsonDocument> documents);
        List<JsonNode> GetJsonNodeList();
        List<BsonDocument> GetBsonDocumentList();
    }
}
