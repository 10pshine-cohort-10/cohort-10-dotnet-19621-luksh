using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Dtos;

public class CategoryRequest
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}