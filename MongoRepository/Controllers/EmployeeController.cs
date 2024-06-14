using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoRepository.Infrastructure.Interfaces;
using MongoRepository.Infrastructure.Models;

namespace MongoRepository.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository  _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _employeeRepository.FilterByAsync(x => true, cancellationToken));
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetEmployeeAsync(string Id, CancellationToken cancellationToken)
        {
            return Ok(await _employeeRepository.FindByIdAsync(Id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAsync(Employees employee, CancellationToken cancellationToken)
        {
            IClientSessionHandle session = await _employeeRepository.BeginSessionAsync(cancellationToken);
            try
            {
                _employeeRepository.BeginTransaction(session);
                await _employeeRepository.InsertOneAsync(employee, session, cancellationToken);
                await _employeeRepository.CommitTransactionAsync(session, cancellationToken);
                _employeeRepository.DisposeSession(session);
                return Ok(true);
            }
            catch (Exception ex)
            {
                await _employeeRepository.RollbackTransactionAsync(session, cancellationToken);
                _employeeRepository.DisposeSession(session);
                return BadRequest(ex);
            }
        }
    }
}
