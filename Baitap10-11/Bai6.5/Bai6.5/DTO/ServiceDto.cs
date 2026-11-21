namespace Bai6._5.DTO;

public sealed class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

    public override string ToString() => Name;
}
