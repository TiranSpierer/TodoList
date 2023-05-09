using MongoDB.Bson;
using ToDoList.Core.DataModels;

namespace ToDoList.Core.Interfaces;

public interface IToDoTaskService
{
    Task<IEnumerable<ToDoTask>> GetAllToDoTasksAsync();
    Task<ToDoTask> GetToDoTaskByIdAsync(ObjectId id);
    Task AddToDoTaskAsync(ToDoTask task);
    Task UpdateTaskAsync(ToDoTask task);
    Task DeleteTaskAsync(ObjectId id);
}