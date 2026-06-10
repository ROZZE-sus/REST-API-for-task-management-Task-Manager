using System.ComponentModel.DataAnnotations;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.DTOs;

public class RegisterRequest
{
    [Required, MinLength(3)]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CreateTaskRequest
{
    [Required, MinLength(1)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public string? Category { get; set; }
    public DateTime? Deadline { get; set; }
}

public class UpdateTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TodoStatus? Status { get; set; }
    public TaskPriority? Priority { get; set; }
    public string? Category { get; set; }
    public DateTime? Deadline { get; set; }
}

public class TaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string? Category { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class TaskFilterRequest
{
    public TodoStatus? Status { get; set; }
    public TaskPriority? Priority { get; set; }
    public string? Category { get; set; }
}
