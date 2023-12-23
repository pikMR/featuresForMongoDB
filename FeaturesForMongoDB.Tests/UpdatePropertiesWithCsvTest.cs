using FeaturesForMongoDB.Impl;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using Moq;
using System.IO.Abstractions;

namespace FeaturesForMongoDB.Tests
{
    internal class UpdatePropertiesWithCsvTest
    {
        private Mock<IOptions<OptionsFile>> options;
        private Mock<IFileSystem> fileSystem;

        [SetUp]
        public void Setup()
        {
            fileSystem = new Mock<IFileSystem>();
            options = new Mock<IOptions<OptionsFile>>();
            fileSystem.Setup(x => x.File.ReadAllLines(It.IsAny<string>()))
                .Returns(new string[] { 
                    "Name",
                    "Maria",
                    "Luisa",
                    "Dolores"
                });
        }

        [Test]
        public void It_should_update_3_bsonDocuments_with_the_values_of_csv()
        {
            var optionsFile = new OptionsFile()
            {
                FilePath = "C:\\Files",
                CsvFile = "Names.csv"
            };

            options.Setup(x => x.Value).Returns(optionsFile);
            var feature = new UpdatePropertiesWithCsv(options.Object, fileSystem.Object);
            var documents = BsonHelper.GetListBsonDocument();
            feature.UpdateDocuments(documents);
            Assert.That(documents, Has.Count.EqualTo(3));
            Assert.That(documents[0]["Name"].ToString(), Is.EqualTo("Maria"));
            Assert.That(documents[1]["Name"].ToString(), Is.EqualTo("Luisa"));
            Assert.That(documents[2]["Name"].ToString(), Is.EqualTo("Dolores"));
            Assert.Pass("update_3_bsonDocuments_with_the_values_of_csv");
        }

        [Test]
        public void It_should_update_3_jsonNodes_with_the_values_of_csv()
        {
            JsonWriterSettings jsonWriterSettings = new() { OutputMode = JsonOutputMode.CanonicalExtendedJson };

            var optionsFile = new OptionsFile()
            {
                FilePath = "C:\\Files",
                CsvFile = "Names.csv"
            };

            options.Setup(x => x.Value).Returns(optionsFile);
            var jsonNode = BsonHelper.GetListJsonDocument();
            var feature = new UpdatePropertiesWithCsv(options.Object, fileSystem.Object);
            feature.UpdateJson(jsonNode);
            Assert.That(jsonNode, Has.Count.EqualTo(3));
            Assert.That(jsonNode[0]["Name"].ToString(), Is.EqualTo("Maria"));
            Assert.That(jsonNode[1]["Name"].ToString(), Is.EqualTo("Luisa"));
            Assert.That(jsonNode[2]["Name"].ToString(), Is.EqualTo("Dolores"));
            Assert.Pass("update_3_jsonNodes_with_the_values_of_csv");
        }
    }
}
