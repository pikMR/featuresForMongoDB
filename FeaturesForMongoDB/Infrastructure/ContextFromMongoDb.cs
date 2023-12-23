using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using FeaturesForMongoDB.Abstrations;

namespace FeaturesForMongoDB.Infrastructure
{
    public class ContextFromMongoDb : IContext
    {
        private readonly OptionsFile _options;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public ContextFromMongoDb(IOptions<OptionsFile> options)
        {
            _options = options.Value;
            _client = new MongoClient(_options.MongoSettings.Connection);
            _database = _client.GetDatabase(_options.MongoSettings.DatabaseName);
        }

        public void CreateBsonDocumentList(List<BsonDocument> documents) 
        {
            var collection = _database.GetCollection<BsonDocument>(_options.MongoSettings.Collection);
            collection.InsertMany(documents);
        }

        public void UpdateBsonDocumentList(List<BsonDocument> documents)
        {
            var collection = _database.GetCollection<BsonDocument>(_options.MongoSettings.Collection);

            foreach(var document in documents)
            {
                var filter = Builders<BsonDocument>.Filter.Eq(e => e["_id"], document["_id"]);
                collection.ReplaceOne(filter, document);
            }
        }

        public List<JsonNode> GetJsonNodeList()
        {
            JsonWriterSettings jsonWriterSettings = new() { OutputMode = JsonOutputMode.CanonicalExtendedJson };
            var documents = GetBsonDocumentList();
            return documents.ConvertAll(x => JsonNode.Parse(x.ToJson(jsonWriterSettings)));
        }

        public List<BsonDocument> GetBsonDocumentList()
        {
            var collection = _database.GetCollection<BsonDocument>(_options.MongoSettings.Collection);
            List<BsonDocument> documents;

            var filter = (string.IsNullOrEmpty(_options.MongoSettings.FilterName) || string.IsNullOrEmpty(_options.MongoSettings.FilterValue)) ?
             "{}" : Builders<BsonDocument>.Filter.Eq(_options.MongoSettings.FilterName, _options.MongoSettings.FilterValue);

            if (_options.MongoSettings.SortAsc == null)
            {
                documents = collection.Find(filter).ToList();
            }
            else
            {
                if (_options.MongoSettings.SortAsc == true)
                {
                    documents = collection.Find(filter).SortBy(bson => bson[_options.MongoSettings.OrderBy]).ToList();
                }
                else
                {
                    documents = collection.Find(filter).SortBy(bson => bson[_options.MongoSettings.OrderBy]).ThenByDescending(bson => bson[_options.MongoSettings.OrderBy]).ToList();
                }
            }

            return documents;
        }

    }
}
