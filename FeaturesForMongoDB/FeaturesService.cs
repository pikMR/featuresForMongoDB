using System.Text.Json.Nodes;
using FeaturesForMongoDB.Abstrations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace FeaturesForMongoDB
{
    public class FeaturesService
    {
        private readonly IEnumerable<IUpdateFeature> _featuresToUpdate;
        private readonly IEnumerable<ICreateFeature> _featuresToCreate;
        private readonly IContext _context;
        private readonly OptionsFile _options;

        public FeaturesService(IEnumerable<IUpdateFeature> featuresToUpdate, IEnumerable<ICreateFeature> featuresToCreate, IOptions<OptionsFile> options, IContext context)
        {
            _featuresToCreate = featuresToCreate;
            _featuresToUpdate = featuresToUpdate;
            _options = options.Value;
            _context = context;
        }

        /// <summary>
        /// Execute some features on jsonNode
        /// </summary>
        /// <param name="jsonNodeElements"></param>
        /// <param name="element"></param>
        public void CreateElements(out List<BsonDocument> documents)
        {
            documents = _context.GetBsonDocumentList();
            foreach (var feature in _featuresToCreate)
            {
                if (_options.ImplementationsToCreate?.Contains(feature.Name) == true)
                {
                    Console.WriteLine($"Run feature: {feature.Name}");
                    feature.CreateDocuments(documents);
                    _context.CreateBsonDocumentList(documents);
                }
            }
        }

        /// <summary>
        /// Execute some features on jsonNode
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="element"></param>
        public void UpdateElements(List<BsonDocument> documents)
        {
            foreach (var feature in _featuresToUpdate)
            {
                if (_options.ImplementationsToUpdate?.Contains(feature.Name) == true)
                {
                    Console.WriteLine($"Run feature: {feature.Name}");
                    feature.UpdateDocuments(documents);
                    _context.UpdateBsonDocumentList(documents);
                }
            }
        }

        /// <summary>
        /// Execute some features on jsonNode
        /// </summary>
        /// <param name="jsonNodes"></param>
        /// <param name="element"></param>
        public void CreateElements(out List<JsonNode> jsonNodes)
        {
            jsonNodes = _context.GetJsonNodeList();

            foreach (var feature in _featuresToCreate)
            {
                if (_options.ImplementationsToCreate?.Contains(feature.Name) == true)
                {
                    Console.WriteLine($"Run feature: {feature.Name}");
                    feature.CreateJson(jsonNodes);
                }
            }
        }

        /// <summary>
        /// Execute some features on jsonNode
        /// </summary>
        /// <param name="jsonNodes"></param>
        /// <param name="element"></param>
        public void UpdateElements(List<JsonNode> jsonNodes)
        {
            foreach (var feature in _featuresToUpdate)
            {
                if (_options.ImplementationsToUpdate?.Contains(feature.Name) == true)
                {
                    Console.WriteLine($"Run feature: {feature.Name}");
                    feature.UpdateJson(jsonNodes);
                }
            }
        }
    }
}

