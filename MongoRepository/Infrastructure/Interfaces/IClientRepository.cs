using MongoRepository.Infrastructure.Models;
using MongoRepository.Interfaces;

namespace MongoRepository.Infrastructure.Interfaces
{
    public interface IClientRepository : IMongoRepository<Clients>
    {
    }
}
