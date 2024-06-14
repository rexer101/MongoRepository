using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        [BsonElement()]
        DateTime CreatedAt { get; }

        DateTime ModifiedAt { get; set; }
    }
}
