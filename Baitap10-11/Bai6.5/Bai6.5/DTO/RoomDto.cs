namespace Bai6._5.DTO;

public sealed class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "Thường";
    public decimal HourlyRate { get; set; }
    public int? ActiveInvoiceId { get; set; }
    public DateTime? StartedAt { get; set; }

    public bool IsOccupied => ActiveInvoiceId is not null;
}
