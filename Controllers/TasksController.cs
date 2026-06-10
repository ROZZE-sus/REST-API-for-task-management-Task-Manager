using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class TasksController : ControllerBase
{
    private readonly AppDbContext _db;

    public TasksController(AppDbContext db)
    {
        _db = db;
    }

    private int GetUserId()
        => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static TaskResponse MapToResponse(TaskItem task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status.ToString(),
        Priority = task.Priority.ToString(),
        Category = task.Category,
        Deadline = task.Deadline,
        CreatedAt = task.CreatedAt,
        UpdatedAt = task.UpdatedAt
    };

    [HttpGet]
    [ProducesResponseType(typeof(List<TaskResponse>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] TaskFilterRequest filter)
    {
        var query = _db.Tasks.Where(t => t.UserId == GetUserId());

        if (filter.Status.HasValue)
            query = query.Where(t => t.Status == filter.Status.Value);

        if (filter.Priority.HasValue)
            query = query.Where(t => t.Priority == filter.Priority.Value);

        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(t => t.Category == filter.Category);

        var tasks = await query
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => MapToResponse(t))
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == GetUserId());

        if (task == null)
            return NotFound(new { message = "Task not found." });

        return Ok(MapToResponse(task));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TaskResponse), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Category = request.Category,
            Deadline = request.Deadline,
            UserId = GetUserId()
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return StatusCode(201, MapToResponse(task));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TaskResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
    {
        var task = await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == GetUserId());

        if (task == null)
            return NotFound(new { message = "Task not found." });

        if (request.Title != null) task.Title = request.Title;
        if (request.Description != null) task.Description = request.Description;
        if (request.Status.HasValue) task.Status = request.Status.Value;
        if (request.Priority.HasValue) task.Priority = request.Priority.Value;
        if (request.Category != null) task.Category = request.Category;
        if (request.Deadline.HasValue) task.Deadline = request.Deadline.Value;

        task.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return Ok(MapToResponse(task));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == GetUserId());

        if (task == null)
            return NotFound(new { message = "Task not found." });

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<string>), 200)]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _db.Tasks
            .Where(t => t.UserId == GetUserId() && t.Category != null)
            .Select(t => t.Category!)
            .Distinct()
            .ToListAsync();

        return Ok(categories);
    }
}
