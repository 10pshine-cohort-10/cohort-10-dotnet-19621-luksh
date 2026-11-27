namespace TaskManagement.Api.Services;

public enum ServiceStatus
{
    Success,
    NotFound,
    Invalid
}

public record ServiceResult<T>(ServiceStatus Status, T? Value = default, string? Error = null)
{
    public static ServiceResult<T> Ok(T value) => new(ServiceStatus.Success, value);
    public static ServiceResult<T> NotFound() => new(ServiceStatus.NotFound);
    public static ServiceResult<T> Invalid(string error) => new(ServiceStatus.Invalid, default, error);
}