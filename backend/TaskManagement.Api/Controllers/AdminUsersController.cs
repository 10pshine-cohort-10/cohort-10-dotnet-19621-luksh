using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Dtos;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
    private readonly IUserService _userService;

    public AdminUsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetUsersAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);

        return result.Status == ServiceStatus.Success
            ? StatusCode(StatusCodes.Status201Created, result.Value)
            : BadRequest(new { message = result.Error });
    }
}