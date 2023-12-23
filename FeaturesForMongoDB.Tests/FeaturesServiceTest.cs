using FeaturesForMongoDB.Abstrations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Moq;

namespace FeaturesForMongoDB.Tests
{
    internal class FeaturesServiceTest
    {
        private Mock<IEnumerable<IUpdateFeature>> _featuresToUpdate;
        private Mock<IEnumerable<ICreateFeature>> _featuresToCreate;
        private Mock<IContext> _context;
        private Mock<IOptions<OptionsFile>> _options;

        [SetUp]
        public void Setup()
        {
            _options = new Mock<IOptions<OptionsFile>>();
            _featuresToCreate = new Mock<IEnumerable<ICreateFeature>>();
            _featuresToUpdate = new Mock<IEnumerable<IUpdateFeature>>();
            _context = new Mock<IContext>();
        }

        [Test]
        public void It_should_throw_exception_when_ImplementationsToCreate_notExist_in_appsettings()
        {
           var service = new FeaturesService(_featuresToUpdate.Object, _featuresToCreate.Object, _options.Object, _context.Object);
           var documents = new List<BsonDocument>();
           Assert.Throws<Exception>(() => service.CreateElements(out documents));
        }
    }
}
