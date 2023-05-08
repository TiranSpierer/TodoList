

using MongoDB.Bson;
using MongoDB.Driver;
using ToDoList.Core.Interfaces;

namespace ToDoList.Infrastructure.Data;

public class MongoRepository<T> : IRepository<T> where T : IDataModel
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T item)
    {
        await _collection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(ObjectId id, T item)
    {
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), item);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
    }
}