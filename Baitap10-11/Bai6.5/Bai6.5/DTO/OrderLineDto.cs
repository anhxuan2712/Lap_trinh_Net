namespace Bai6._5.DTO;

/// <summary>Một dòng dịch vụ đã gọi trong hóa đơn.</summary>
public sealed class OrderLineDto
{
    public int ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal Amount => Quantity * UnitPrice;
}
