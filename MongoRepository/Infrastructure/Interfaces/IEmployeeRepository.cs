using MongoRepository.Infrastructure.Models;
using MongoRepository.Interfaces;

namespace MongoRepository.Infrastructure.Interfaces
{
    public interface IEmployeeRepository : IMongoRepository<Employees>
    {

    }
}
