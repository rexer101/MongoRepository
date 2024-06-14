using MongoDB.Driver;
using System.Linq.Expressions;

namespace MongoRepository.Interfaces
{
    public interface IMongoRepository<TDocument> : ISession where TDocument : IDocument
    {
        IQueryable<TDocument> AsQueryable();

        Task<IEnumerable<TDocument>> FilterByAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken);

        Task<IEnumerable<TProjected>> FilterByAsync<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            FindOptions<TDocument, TProjected> projectionExpression,
            CancellationToken cancellationToken);

        Task<TDocument> FindOneAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken);

        Task<TDocument> FindByIdAsync(
            string id,
            CancellationToken cancellationToken);

        Task InsertOneAsync(
            TDocument document,
            IClientSessionHandle clientSessionHandle,
            CancellationToken cancellationToken);

        Task InsertManyAsync(
            ICollection<TDocument> documents,
            IClientSessionHandle clientSessionHandle,
            CancellationToken cancellationToken);

        Task<bool> ReplaceOneAsync(
            TDocument document,
            IClientSessionHandle clientSessionHandle,
            CancellationToken cancellationToken);

        Task<bool> DeleteOneAsync(
            string id,
            IClientSessionHandle clientSessionHandle,
            CancellationToken cancellationToken);

        Task<bool> DeleteManyAsync(
            Expression<Func<TDocument,
            bool>> filterExpression,
            IClientSessionHandle clientSessionHandle,
            CancellationToken cancellationToken);
    }
}
