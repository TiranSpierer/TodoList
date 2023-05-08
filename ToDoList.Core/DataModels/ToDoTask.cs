using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ToDoList.Core.Interfaces;

namespace ToDoList.Core.DataModels;

public class ToDoTask : IDataModel
{
    public ObjectId Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    public DateTime? DueDate { get; set; }
    public ToDoTaskPriority Priority { get; set; }
    public ToDoTaskStatus Status { get; set; }
}

public enum ToDoTaskPriority
{
    Low,
    Medium,
    High
}

public enum ToDoTaskStatus
{
    NotStarted,
    InProgress,
    Completed
}