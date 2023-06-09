﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ToDoList.Core.DataModels;
using ToDoList.Core.Interfaces;

namespace ToDoList.Web.Controllers;
public class ToDoTaskController : Controller
{
    private readonly IToDoTaskService _toDoTaskService;

    public ToDoTaskController(IToDoTaskService toDoTaskService)
    {
        _toDoTaskService = toDoTaskService;
    }

    // GET: Task
    public async Task<IActionResult> Index()
    {
        var tasks = await _toDoTaskService.GetAllToDoTasksAsync();
        return View(tasks);
    }

    // GET: Task/Details/5
    public async Task<IActionResult> Details(ObjectId? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _toDoTaskService.GetToDoTaskByIdAsync(id.Value);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // GET: Task/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Task/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,Priority,Status")] ToDoTask toDoTask)
    {
        if (ModelState.IsValid)
        {
            await _toDoTaskService.AddToDoTaskAsync(toDoTask);
            return RedirectToAction(nameof(Index));
        }
        return View(toDoTask);
    }

    // GET: Task/Edit/5
    public async Task<IActionResult> Edit(ObjectId? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _toDoTaskService.GetToDoTaskByIdAsync(id.Value);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    // POST: Task/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ObjectId id, [Bind("Id,Title,Description,DueDate,Priority,Status")] ToDoTask task)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _toDoTaskService.UpdateTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    // GET: Task/Delete/5
    public async Task<IActionResult> Delete(ObjectId? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _toDoTaskService.GetToDoTaskByIdAsync(id.Value);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Task/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(ObjectId id)
    {
        await _toDoTaskService.DeleteTaskAsync(id);
        return RedirectToAction(nameof(Index));
    }
}