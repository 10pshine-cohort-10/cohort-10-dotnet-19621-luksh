using TaskManagement.Api.Dtos;

namespace TaskManagement.Api.Services;

public interface IUserService
{
    Task<IReadOnlyList<UserResponse>> GetUsersAsync();
    Task<ServiceResult<UserResponse>> CreateUserAsync(CreateUserRequest request);
}