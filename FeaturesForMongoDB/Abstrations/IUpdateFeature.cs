using System.Text.Json.Nodes;
using MongoDB.Bson;

namespace FeaturesForMongoDB.Abstrations
{
    public interface IUpdateFeature
    {
        string Name { get; }

        /// <summary>
        /// Executes a functionality on a list to update jsons files
        /// </summary>
        /// <param name="jsonNodes"></param>
        void UpdateJson(List<JsonNode> jsonNodes);

        /// <summary>
        /// Executes a functionality on a list to update documents db
        /// </summary>
        /// <param name="documents"></param>
        void UpdateDocuments(List<BsonDocument> documents);
    }
}
