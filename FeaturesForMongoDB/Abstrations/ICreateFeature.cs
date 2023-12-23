using System.Text.Json.Nodes;
using MongoDB.Bson;

namespace FeaturesForMongoDB.Abstrations
{
    public interface ICreateFeature
    {
        string Name { get; }

        /// <summary>
        /// Executes a functionality on a list to create elements to collection in db
        /// </summary>
        /// <param name="jsonNodes"></param>
        void CreateDocuments(List<BsonDocument> elements);

        /// <summary>
        /// Executes a functionality on a list to create elements to json file
        /// </summary>
        /// <param name="jsonNodes"></param>
        void CreateJson(List<JsonNode> jsonNodes);
    }
}
