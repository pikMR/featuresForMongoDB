using System.IO.Abstractions;
using System.Text.Json.Nodes;
using FeaturesForMongoDB.Abstrations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FeaturesForMongoDB.Impl
{
    public class UpdatePropertiesWithCsv : IUpdateFeature
    {
        private readonly OptionsFile _options;
        private readonly IEnumerable<string[]> _lines;
        private readonly int _numElementsCsv;
        private readonly string[] _head;

        public UpdatePropertiesWithCsv(IOptions<OptionsFile> options, IFileSystem fileSystem)
        {
            _options = options.Value;
            _lines = fileSystem.File.ReadAllLines($"{_options.FilePath}/{_options.CsvFile}").Select(a => a.Split(';'));
            var csv = from line in _lines
                      select (from piece in line
                              select piece);

            _numElementsCsv = _lines.Count();
            _head = csv.First().ToArray();
        }

        public string Name => "UpdatePropertiesWithCsv";

        public void UpdateDocuments(List<BsonDocument> documents)
        {
            for (int i = 0; i < _head.Length; i++)
            {
                var prop = _head[i];
                for (int j = 0; j < documents.Count; j++)
                {
                    if (_numElementsCsv > (j + 1))
                    {
                        documents[j].SetElement(new BsonElement(prop, _lines.ElementAt(j + 1).FirstOrDefault() ?? documents[j].GetValue(prop)));
                    }
                }
            }
        }

        public void UpdateJson(List<JsonNode> jsonNodes)
        {
            for (int i = 0; i < _head.Length; i++)
            {
                var prop = _head[i];
                for (int j = 0; j < jsonNodes.Count; j++)
                {
                    if (_numElementsCsv > (j + 1))
                    {
                        jsonNodes[j][prop] = _lines.ElementAt(j + 1).FirstOrDefault() ?? jsonNodes[j][prop];
                    }
                }
            }
        }
    }
}
