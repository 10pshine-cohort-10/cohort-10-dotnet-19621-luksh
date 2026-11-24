using TaskManagement.Api.Dtos;
using TaskManagement.Api.Enums;

namespace TaskManagement.Api.Services;

public interface ITaskService
{
    Task<IReadOnlyList<TaskResponse>> GetTasksAsync(
        int userId, bool isPrivileged, TaskItemStatus? status, TaskPriority? priority, int? categoryId);

    Task<TaskResponse?> GetByIdAsync(int id, int userId, bool isPrivileged);
    Task<ServiceResult<TaskResponse>> CreateAsync(TaskCreateRequest request, int userId, bool isPrivileged);
    Task<ServiceResult<TaskResponse>> UpdateAsync(int id, TaskUpdateRequest request, int userId, bool isPrivileged);
    Task<bool> DeleteAsync(int id, int userId, bool isPrivileged);
    Task<DashboardResponse> GetDashboardAsync(int userId, bool isPrivileged);
}