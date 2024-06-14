using MongoDB.Driver;
using MongoRepository.Implementation;
using MongoRepository.Infrastructure.Interfaces;
using MongoRepository.Infrastructure.Models;

namespace MongoRepository.Infrastructure.Repositories
{
    public class ClientRepository : MongoRepository<Clients>, IClientRepository
    {
        public ClientRepository(IMongoClient mongoClient) : base(mongoClient) { }
    }
}
