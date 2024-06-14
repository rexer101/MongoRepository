using MongoDB.Bson;
using MongoRepository.Attributes;
using MongoRepository.Interfaces;

namespace MongoRepository.Infrastructure.Models
{
    [BsonCollection("Clients")]
    public class Clients : IDocument
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public DateTime ModifiedAt { get; set; }
    }
}
