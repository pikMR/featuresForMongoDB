using System.IO.Abstractions;
using System.Text.Json.Nodes;
using FeaturesForMongoDB;
using FeaturesForMongoDB.Abstrations;
using FeaturesForMongoDB.Impl;
using FeaturesForMongoDB.Infrastructure;
using FeaturesForMongoDB.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var options = new OptionsFile();
        config.Bind(options);

        using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
            {
                services.Configure<OptionsFile>(config);
                services.AddSingleton<ICreateFeature, CreateRandomGuids>();
                services.AddSingleton<IUpdateFeature, UpdatePropertiesWithCsv>();
                services.AddSingleton<FeaturesService>();
                services.AddSingleton<IContext, ContextFromMongoDb>();
                services.AddSingleton<IFileSystem, FileSystem>();
            }).Build();

        var optionsValidator = new ConfigurationValidator().Validate(options);

        if (optionsValidator.IsValid)
        {
            var featuresService = host.Services.GetService<FeaturesService>();

            // execution of functions with implementation of tasks on the collection database
            if (options.UpdateCollection && featuresService != null)
            {
                featuresService.CreateElements(out List<BsonDocument> documents);
                featuresService.UpdateElements(documents);
            }

            // execution of functions with implementation of tasks on the json
            if (options.CreateJson && featuresService != null)
            {
                featuresService.CreateElements(out List<JsonNode> jsonNodes);
                featuresService.UpdateElements(jsonNodes);

                // write json file
                var jsonArray = new JsonArray(jsonNodes.ToArray());
                string defaultPath = $"{options.MongoSettings.DatabaseName}.{options.MongoSettings.Collection}-{DateTime.Now:yyMMdd-hmmss}.json";
                File.WriteAllText(string.IsNullOrEmpty(options.FilePath) ? defaultPath : $"{options.FilePath}/{defaultPath}", jsonArray.ToJsonString());
            }
            Console.WriteLine("File generation with the following configuration : \n" + options.ToString());
        }
        else
        {
            optionsValidator.Errors.ForEach(x => Console.WriteLine($"{x.ErrorCode} - {x.ErrorMessage}"));
        }



    }
}