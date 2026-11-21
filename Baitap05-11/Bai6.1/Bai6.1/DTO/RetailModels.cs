namespace Bai6._1.DTO;

public sealed record Product(string Code, string Name, decimal Price, int Stock);

public sealed record Customer(int Id, string Name, string? Phone, int Points);

public sealed record Employee(int Id, string Name, string? Phone, string? Role);

public sealed record CartItem(string ProductCode, string ProductName, int Quantity, decimal UnitPrice)
{
    public decimal LineTotal => Quantity * UnitPrice;
}
