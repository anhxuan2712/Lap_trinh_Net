namespace Bai6._5.DTO;

/// <summary>Hóa đơn đang mở, kèm giá phòng để tính tiền khi thanh toán.</summary>
public sealed class InvoiceDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime StartedAt { get; set; }
    public decimal HourlyRate { get; set; }
}
