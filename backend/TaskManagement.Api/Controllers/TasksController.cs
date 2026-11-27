using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Dtos;
using TaskManagement.Api.Enums;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

      private bool IsPrivileged => User.IsInRole(nameof(UserRole.Admin));

    private bool TryGetUserId(out int userId)
    {
        return int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] TaskItemStatus? status,
        [FromQuery] TaskPriority? priority,
        [FromQuery] int? categoryId)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var tasks = await _taskService.GetTasksAsync(userId, IsPrivileged, status, priority, categoryId);
        return Ok(tasks);
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        return Ok(await _taskService.GetDashboardAsync(userId, IsPrivileged));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var task = await _taskService.GetByIdAsync(id, userId, IsPrivileged);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskCreateRequest request)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var result = await _taskService.CreateAsync(request, userId, IsPrivileged);
        return result.Status switch
        {
            ServiceStatus.Success => CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value),
            ServiceStatus.Invalid => BadRequest(new { message = result.Error }),
            _ => NotFound()
        };
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TaskUpdateRequest request)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var result = await _taskService.UpdateAsync(id, request, userId, IsPrivileged);
        return result.Status switch
        {
            ServiceStatus.Success => Ok(result.Value),
            ServiceStatus.Invalid => BadRequest(new { message = result.Error }),
            _ => NotFound()
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var deleted = await _taskService.DeleteAsync(id, userId, IsPrivileged);
        return deleted ? NoContent() : NotFound();
    }
}