using System.Text.Json.Serialization;

namespace FeaturesForMongoDB
{
    public partial class OptionsFile
    {
        [JsonPropertyName("MongoSettings")]
        public MongoSettings MongoSettings { get; set; }

        [JsonPropertyName("FilePath")]
        public string FilePath { get; set; }

        [JsonPropertyName("UpdateCollection")]
        public bool UpdateCollection { get; set; }

        [JsonPropertyName("CreateJson")]
        public bool CreateJson { get; set; }

        [JsonPropertyName("NumToDuplicate")]
        public ushort NumToDuplicate { get; set; }

        [JsonPropertyName("ImplementationsToUpdate")]
        public string[] ImplementationsToUpdate { get; set; }

        [JsonPropertyName("ImplementationsToCreate")]
        public string[] ImplementationsToCreate { get; set; }

        [JsonPropertyName("CsvFile")]
        public string CsvFile { get; set; }


        public override string ToString()
        {
            return $"{{{nameof(MongoSettings)}={MongoSettings}, {nameof(FilePath)}={FilePath}, {nameof(NumToDuplicate)}={NumToDuplicate.ToString()}, {nameof(ImplementationsToUpdate)}={ImplementationsToUpdate}, {nameof(ImplementationsToCreate)}={ImplementationsToCreate}, {nameof(CsvFile)}={CsvFile}}}";
        }
    }

    public partial class MongoSettings
    {
        [JsonPropertyName("Connection")]
        public string Connection { get; set; }

        [JsonPropertyName("DatabaseName")]
        public string DatabaseName { get; set; }

        [JsonPropertyName("Collection")]
        public string Collection { get; set; }

        [JsonPropertyName("FilterName")]
        public string? FilterName { get; set; }

        [JsonPropertyName("FilterValue")]
        public string? FilterValue { get; set; }

        [JsonPropertyName("OrderBy")]
        public string OrderBy { get; set; }

        [JsonPropertyName("SortAsc")]
        public bool? SortAsc { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Connection)}={Connection}, {nameof(DatabaseName)}={DatabaseName}, {nameof(Collection)}={Collection}, {nameof(FilterName)}={FilterName}, {nameof(FilterValue)}={FilterValue}}}";
        }
    }
}
