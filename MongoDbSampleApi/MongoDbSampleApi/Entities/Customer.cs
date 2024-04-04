using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbSampleApi.Entities
{
    public class Customer : CustomerCredential
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
