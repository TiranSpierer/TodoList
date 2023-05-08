using MongoDB.Bson;

namespace ToDoList.Core.Interfaces;

public interface IRepository<T> where T : IDataModel
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(ObjectId id);
    Task AddAsync(T item);
    Task UpdateAsync(ObjectId id, T item);
    Task DeleteAsync(ObjectId id);
}