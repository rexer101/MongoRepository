using MongoDB.Bson;
using MongoDB.Driver;
using MongoRepository.Attributes;
using MongoRepository.Interfaces;
using System.Linq.Expressions;

namespace MongoRepository.Implementation
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoClient mongoClient)
        {
            _collection = mongoClient.GetDatabase("myDatabase").GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            var name = documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true)?.FirstOrDefault();
            if (name != null)
            {
                return ((BsonCollectionAttribute)name).CollectionName;
            }

            throw new ArgumentException("The collection is unknown");
        }

        public virtual async Task<IClientSessionHandle> BeginSessionAsync(CancellationToken cancellationToken)
        {
            var option = new ClientSessionOptions();
            option.DefaultTransactionOptions = new TransactionOptions();
            return await _collection.Database.Client.StartSessionAsync(option, cancellationToken);
        }

        public virtual void BeginTransaction(IClientSessionHandle clientSessionHandle)
            => clientSessionHandle.StartTransaction();

        public virtual Task CommitTransactionAsync(IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        => clientSessionHandle.CommitTransactionAsync(cancellationToken);

        public virtual Task RollbackTransactionAsync(IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        => clientSessionHandle.AbortTransactionAsync(cancellationToken);

        public virtual void DisposeSession(IClientSessionHandle clientSessionHandle)
        => clientSessionHandle.Dispose();

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual async Task<IEnumerable<TDocument>> FilterByAsync(
            Expression<Func<TDocument, bool>> filterExpression,
            CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(filterExpression, null, cancellationToken)).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TProjected>> FilterByAsync<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            FindOptions<TDocument, TProjected> projectionExpression,
            CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(filterExpression, projectionExpression, cancellationToken)).ToListAsync(cancellationToken);
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression, CancellationToken cancellationToken)
        {
            return await (await _collection.FindAsync(filterExpression, null, cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }


        public virtual async Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return await (await _collection.FindAsync(filter, null, cancellationToken)).SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task InsertOneAsync(TDocument document, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(clientSessionHandle, document, null, cancellationToken);
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        {
            await _collection.InsertManyAsync(clientSessionHandle, documents, null, cancellationToken);
        }

        public virtual async Task<bool> ReplaceOneAsync(TDocument document, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            var updateResult = await _collection.ReplaceOneAsync(clientSessionHandle, filter, document, options: new ReplaceOptions(), cancellationToken);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteOneAsync(string id, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            var deleteResult = await _collection.DeleteOneAsync(clientSessionHandle, filter, null, cancellationToken);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public virtual async Task<bool> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken)
        {
            var deleteResult = await _collection.DeleteManyAsync(clientSessionHandle, filterExpression, null, cancellationToken);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
