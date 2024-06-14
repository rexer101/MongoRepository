using MongoDB.Bson;
using MongoRepository.Attributes;
using MongoRepository.Interfaces;

namespace MongoRepository.Infrastructure.Models
{
    [BsonCollection("Employees")]
    public class Employees : IDocument
    {
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Designation { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public DateTime ModifiedAt { get; set; }
    }
}
