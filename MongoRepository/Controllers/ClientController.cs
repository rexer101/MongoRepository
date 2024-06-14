using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoRepository.Infrastructure.Interfaces;
using MongoRepository.Infrastructure.Models;
using MongoRepository.Infrastructure.Repositories;

namespace MongoRepository.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientsAsync(CancellationToken cancellationToken)
        {
            return Ok(await _clientRepository.FilterByAsync(x => true, cancellationToken));
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetClientAsync(string Id, CancellationToken cancellationToken)
        {
            return Ok(await _clientRepository.FindByIdAsync(Id, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddClientAsync(Clients client, CancellationToken cancellationToken)
        {
            IClientSessionHandle session = await _clientRepository.BeginSessionAsync(cancellationToken);
            try
            {
                _clientRepository.BeginTransaction(session);
                await _clientRepository.InsertOneAsync(client, session, cancellationToken);
                await _clientRepository.CommitTransactionAsync(session, cancellationToken);
                _clientRepository.DisposeSession(session);
                return Ok(true);
            }
            catch (Exception ex)
            {
                await _clientRepository.RollbackTransactionAsync(session, cancellationToken);
                _clientRepository.DisposeSession(session);
                return BadRequest(ex);
            }
        }
    }
}
