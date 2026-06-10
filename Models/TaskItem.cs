namespace TaskManagerAPI.Models;

public enum TodoStatus 
{
    Pending,
    InProgress,
    Done
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TodoStatus Status { get; set; } = TodoStatus.Pending;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public string? Category { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
