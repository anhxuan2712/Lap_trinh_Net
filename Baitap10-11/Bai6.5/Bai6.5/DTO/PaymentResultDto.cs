namespace Bai6._5.DTO;

public sealed class PaymentResultDto
{
    public int InvoiceId { get; set; }
    public decimal RoomTotal { get; set; }
    public decimal ServiceTotal { get; set; }

    public decimal Total => RoomTotal + ServiceTotal;
}
