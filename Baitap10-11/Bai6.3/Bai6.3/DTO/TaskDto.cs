namespace Bai6._3.DTO;

public sealed class TaskDto
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? DueDate { get; set; }
    public string Priority { get; set; } = "Trung bình";
    public string Status { get; set; } = "To Do";
}

public sealed class CompletionReportDto
{
    public string MemberName { get; set; } = "";
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public decimal CompletionRate { get; set; }
}
