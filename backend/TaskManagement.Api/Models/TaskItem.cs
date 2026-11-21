using TaskManagement.Api.Enums;

namespace TaskManagement.Api.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? AssignedToUserId { get; set; }
    public User? AssignedToUser { get; set; }
}