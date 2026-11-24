namespace TaskManagement.Api.Dtos;

public class DashboardResponse
{
    public int Completed { get; set; }
    public int InProgress { get; set; }
    public int Pending { get; set; }
    public int Total { get; set; }
}