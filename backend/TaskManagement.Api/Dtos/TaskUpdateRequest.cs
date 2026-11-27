using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Enums;

namespace TaskManagement.Api.Dtos;

public class TaskUpdateRequest
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public int? CategoryId { get; set; }
    public int? AssignedToUserId { get; set; }
}