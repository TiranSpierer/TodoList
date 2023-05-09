using MongoDB.Bson;
using ToDoList.Core.DataModels;
using ToDoList.Core.Interfaces;

namespace ToDoList.Core.Services;

public class ToDoTaskService : IToDoTaskService
{
    private readonly IRepository<ToDoTask> _repository;

    public ToDoTaskService(IRepository<ToDoTask> taskRepository)
    {
        _repository = taskRepository;
    }

    public async Task<IEnumerable<ToDoTask>> GetAllToDoTasksAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ToDoTask> GetToDoTaskByIdAsync(ObjectId id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddToDoTaskAsync(ToDoTask task)
    {
        await _repository.AddAsync(task);
    }

    public async Task UpdateTaskAsync(ToDoTask task)
    {
        await _repository.UpdateAsync(task.Id, task);
    }

    public async Task DeleteTaskAsync(ObjectId id)
    {
        await _repository.DeleteAsync(id);
    }

}