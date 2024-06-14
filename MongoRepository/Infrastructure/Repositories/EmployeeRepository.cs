using MongoDB.Driver;
using MongoRepository.Implementation;
using MongoRepository.Infrastructure.Interfaces;
using MongoRepository.Infrastructure.Models;

namespace MongoRepository.Infrastructure.Repositories
{
    public class EmployeeRepository : MongoRepository<Employees>, IEmployeeRepository
    {
        public EmployeeRepository(IMongoClient mongoClient) : base(mongoClient) { }
    }
}
