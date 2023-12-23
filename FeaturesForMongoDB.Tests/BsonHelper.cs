using MongoDB.Bson;
using System.Text.Json.Nodes;

namespace FeaturesForMongoDB.Tests
{
    internal static class BsonHelper
    {
        public static List<JsonNode> GetListJsonDocument()
        {
            var jsonObject1 = new JsonObject
            {
                ["_id"] = new JsonObject
                {
                    ["$binary"] = new JsonObject
                    {
                        ["base64"] = "T76zfffOZUiXVk+6XKniKw==",
                        ["subType"] = "03"
                    }
                },
                ["Name"] = "Paco",
                ["File"] = new JsonArray
            {
                new JsonObject
                {
                    ["Url"] = "b85497ce3.png",
                    ["FileKey"] = "imagen"
                }
            }
            };

            var jsonObject2 = new JsonObject
            {
                ["_id"] = new JsonObject
                {
                    ["$binary"] = new JsonObject
                    {
                        ["base64"] = "lYOZLY6IskefYNozHYFGkQ==",
                        ["subType"] = "03"
                    }
                },
                ["Name"] = "Juan",
                ["File"] = new JsonArray
            {
                new JsonObject
                {
                    ["Url"] = "20ae7ef.png",
                    ["FileKey"] = "imagen"
                }
            }
            };

            var jsonObject3 = new JsonObject
            {
                ["_id"] = new JsonObject
                {
                    ["$binary"] = new JsonObject
                    {
                        ["base64"] = "NyjGbl3YIEad8+oRArebew==",
                        ["subType"] = "03"
                    }
                },
                ["Name"] = "Miguel",
                ["File"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["Url"] = "40ae4e3.png",
                        ["FileKey"] = "imagen"
                    }
                }
            };
            return new List<JsonNode> { jsonObject1, jsonObject2, jsonObject3 };
            
        }
        public static List<BsonDocument> GetListBsonDocument()
        {
            BsonDocument bsonDocument1 = new BsonDocument
            {
                { "_id", new BsonDocument
                    {
                        { "$binary", new BsonDocument
                            {
                                { "base64", "T76zfffOZUiXVk+6XKniKw==" },
                                { "subType", "03" }
                            }
                        }
                    }
                },
                { "Name", "Paco" },
                { "File", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "Url", "427c828a-d90b-40fd-8420-fdcb85497ce3.png" },
                            { "FileKey", "imagen" }
                        }
                    }
                }
            };

            BsonDocument bsonDocument2 = new BsonDocument
                            {
                { "_id", new BsonDocument
                    {
                        { "$binary", new BsonDocument
                            {
                                { "base64", "lYOZLY6IskefYNozHYFGkQ==" },
                                { "subType", "03" }
                            }
                        }
                    }
                },
                { "Name", "Pedro" },
                { "File", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "Url", "427c828a-d90b-40fd-8420-fdcb85497ce3.png" },
                            { "FileKey", "imagen" }
                        }
                    }
                }
            };

            BsonDocument bsonDocument3 = new BsonDocument{
                { "_id", new BsonDocument
                    {
                        { "$binary", new BsonDocument
                            {
                                { "base64", "AniuHf2KRE2eB7Du6APz6Q==" },
                                { "subType", "03" }
                            }
                        }
                    }
                },
                { "Name", "Juan" },
                { "File", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "Url", "427c828a-d90b-40fd-8420-fdcb85497ce3.png" },
                            { "FileKey", "imagen" }
                        }
                    }
                }
            };

            return new List<BsonDocument>() { bsonDocument1, bsonDocument2, bsonDocument3 };
        }
    }
}
