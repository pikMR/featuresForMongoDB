using System.Text.Json.Nodes;
using FeaturesForMongoDB.Abstrations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace FeaturesForMongoDB.Impl
{
    public class CreateRandomGuids : ICreateFeature
    {
        private readonly OptionsFile _options;
        public CreateRandomGuids(IOptions<OptionsFile> options)
        {
            _options = options.Value;
        }

        public string Name => "CreateRandomGuids";

        public void CreateDocuments(List<BsonDocument> documents)
        {
            var document = documents.First();
            documents.RemoveAll(x => x != null);
            for (var j = 0; j < _options.NumToDuplicate; j++)
            {
                var cloneDocument = document.DeepClone().AsBsonDocument;
                var newGuid = new BsonBinaryData(Guid.NewGuid(), GuidRepresentation.CSharpLegacy);
                cloneDocument.SetElement(new BsonElement("_id", newGuid));
                documents.Add(cloneDocument);
            }
        }

        public void CreateJson(List<JsonNode> jsonNodes)
        {
            var element = jsonNodes.First();
            jsonNodes.RemoveAll(x => x != null);

            for (var j = 0; j < _options.NumToDuplicate; j++)
            {
                var cloneElement = element.DeepClone();
                var newGuid = Guid.NewGuid();
                var base64Encoded = Convert.ToBase64String(newGuid.ToByteArray());
                Console.WriteLine($"{newGuid} - {base64Encoded}");
                cloneElement!["_id"]!["$binary"]!["base64"] = base64Encoded;
                jsonNodes.Add(cloneElement);
            }
        }
    }
}
