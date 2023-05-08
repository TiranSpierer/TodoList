using MongoDB.Bson;
using ToDoList.Core.DataModels;

namespace ToDoList.Core.Interfaces;

public interface IToDoTaskService
{
    Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
    Task<ToDoTask> GetTaskByIdAsync(ObjectId id);
    Task AddTaskAsync(ToDoTask task);
    Task UpdateTaskAsync(ToDoTask task);
    Task DeleteTaskAsync(ObjectId id);
}