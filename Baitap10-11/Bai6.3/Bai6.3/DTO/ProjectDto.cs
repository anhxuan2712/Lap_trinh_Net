namespace Bai6._3.DTO;

public sealed class ProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = "Đang chạy";
    public override string ToString() => $"{Id} - {Name}";
}
