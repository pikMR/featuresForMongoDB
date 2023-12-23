using FeaturesForMongoDB.Impl;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Moq;
using System.Text.Json.Nodes;

namespace FeaturesForMongoDB.Tests;

internal class CreateRandomGuidsTest
{
    private Mock<IOptions<OptionsFile>> options;
    private List<BsonDocument> documents;

    [SetUp]
    public void Setup()
    {
        options = new Mock<IOptions<OptionsFile>>();
        documents = BsonHelper.GetListBsonDocument();
    }

    [Test]
    public void It_should_create_10_bsonDocuments_with_random_guid_and_same_properties_than_first_element()
    {
        var optionsFile = new OptionsFile()
        {
            NumToDuplicate = 10
        };

        options.Setup(x => x.Value).Returns(optionsFile);
        var feature = new CreateRandomGuids(options.Object);
        feature.CreateDocuments(documents);
        Assert.That(documents, Has.Count.EqualTo(10));
        Assert.IsTrue(documents.All(x => x["Name"] == "Paco"));
        Assert.Pass("create_10_elements_with_random_guid_and_same_properties_than_first_element");
    }

    [Test]
    public void It_should_create_10_jsonNodes_with_random_guid_and_same_properties_than_first_element()
    {
        JsonWriterSettings jsonWriterSettings = new() { OutputMode = JsonOutputMode.CanonicalExtendedJson };

        var optionsFile = new OptionsFile()
        {
            NumToDuplicate = 10
        };

        options.Setup(x => x.Value).Returns(optionsFile);
        var jsonNode = documents.ConvertAll(x => JsonNode.Parse(x.ToJson(jsonWriterSettings)));
        var feature = new CreateRandomGuids(options.Object);
        feature.CreateJson(jsonNode);
        Assert.That(jsonNode, Has.Count.EqualTo(10));
        Assert.IsTrue(jsonNode.All(x => x["Name"].ToString() == "Paco"));
        Assert.Pass("create_10_jsonNodes_with_random_guid_and_same_properties_than_first_element");
    }
}